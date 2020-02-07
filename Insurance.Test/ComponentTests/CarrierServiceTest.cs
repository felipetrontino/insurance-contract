using FluentAssertions;
using Insurance.Core.Domain.Common;
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Exceptions;
using Insurance.Core.Domain.Models.InputModel;
using Insurance.Core.Domain.Models.ViewModel;
using Insurance.Core.Domain.Services;
using Insurance.Core.Infra.Data;
using Insurance.Test.Common;
using Insurance.Test.Mocks;
using Insurance.Test.Mocks.Entities;
using Insurance.Test.Mocks.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.Test.ComponentTests
{
    public class CarrierServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;

        public CarrierServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();
        }

        #region Add

        [Fact]
        public void CarrierAddEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key);

            // act
            Add(model);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        [Fact]
        public void CarrierAddWithInputNull()
        {
            // arrange
            var model = CarrierInputModelMock.Null;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void CarrierAddEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key);
            model.Name = null;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void CarrierAddEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key);
            model.Name = string.Empty;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        private void Add(CarrierInputModel model)
        {
            var service = new CarrierService(MockRepository.GetContext());
            service.Add(model).Wait();
        }

        #endregion Add

        #region Update

        [Fact]
        public void CarrierUpdateEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();
            var model = CarrierInputModelMock.Get(key2);

            // act
            Update(entity.Id, model);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        [Fact]
        public void CarrierUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var model = CarrierInputModelMock.Null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        [Fact]
        public void CarrierUpdateWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            var key2 = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key2);

            // act
            Action action = () => Update(Fake.GetId(key), model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
        }

        [Fact]
        public void CarrierUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key2);
            model.Name = null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        [Fact]
        public void CarrierUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = CarrierInputModelMock.Get(key2);
            model.Name = string.Empty;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        private void Update(Guid id, CarrierInputModel model)
        {
            var service = new CarrierService(MockRepository.GetContext());
            service.Update(id, model).Wait();
        }

        #endregion Update

        #region Get

        [Fact]
        public void CarrierGetEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = Get(entity.Id);

            // assertation
            var viewModelExpected = CarrierViewModelMock.Get(key);
            result.Should().BeEquivalentTo(viewModelExpected);
        }

        [Fact]
        public void CarrierGetEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Get(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);
        }

        [Fact]
        public void CarrierGetEntityWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            // act
            Action action = () => Get(Fake.GetId(key));

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
        }

        private CarrierViewModel Get(Guid id)
        {
            var service = new CarrierService(MockRepository.GetContext());
            return service.Get(id).GetAwaiter().GetResult();
        }

        #endregion Get

        #region GetAll

        [Fact]
        public void CarrierGetAllEntities()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = GetAll();

            // assertation
            var viewModelExpected = CarrierViewModelMock.Get(key);
            result.Should().BeEquivalentTo(new List<CarrierViewModel>() { viewModelExpected });
        }

        [Fact]
        public void CarrierGetAllWithoutEntities()
        {
            // arrange

            // act
            var result = GetAll();

            // assertation
            result.Should().BeEmpty();
        }

        private List<CarrierViewModel> GetAll()
        {
            var service = new CarrierService(MockRepository.GetContext());
            return service.GetAll().GetAwaiter().GetResult();
        }

        #endregion GetAll

        #region Delete

        [Fact]
        public void CarrierDeleteEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisoDeleteEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Delete(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });
        }

        [Fact]
        public void CarrierDeleteEntityWithoutEntity()
        {
            // arrange

            // act
            Action action = () => Delete(Guid.NewGuid());

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void CarrierDeleteEntityWithContract()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            var key2 = Fake.GetKey();

            var entity2 = CarrierMock.Get(key2);
            MockRepository.Add(entity2);

            var contract = ContractMock.Get(entity.Id, entity2.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            var entityExpected = CarrierMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Carrier>() { entityExpected });

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        private void Delete(Guid id)
        {
            var service = new CarrierService(MockRepository.GetContext());
            service.Delete(id).Wait();
        }

        #endregion Delete
    }
}
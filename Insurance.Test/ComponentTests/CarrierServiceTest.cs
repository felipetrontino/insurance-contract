using FluentAssertions;
using Insurance.Core.Domain.Common;
using Insurance.Core.Domain.Entities;
using Insurance.Core.Domain.Exceptions;
using Insurance.Core.Domain.Services;
using Insurance.Core.Infra.Data;
using Insurance.Test.Common;
using Insurance.Test.Mocks;
using Insurance.Test.Mocks.Entities;
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

            var entity = CarrierMock.Get(key);

            // act
            Add(entity);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity });
        }

        [Fact]
        public void CarrierAddWithInputNull()
        {
            // arrange
            var entity = CarrierMock.Null;

            // act
            Action action = () => Add(entity);

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

            var entity = CarrierMock.Get(key);
            entity.Name = null;

            // act
            Action action = () => Add(entity);

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

            var entity = CarrierMock.Get(key);
            entity.Name = string.Empty;

            // act
            Action action = () => Add(entity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEmpty();
        }

        private void Add(Carrier entity)
        {
            var service = new CarrierService(MockRepository.GetContext());
            service.Add(entity).Wait();
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

            var updateEntity = CarrierMock.Get(Fake.GetKey());
            updateEntity.Id = entity.Id;

            // act
            Update(updateEntity);

            // assertation
            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEquivalentToModel(new List<Carrier>() { updateEntity });
        }

        [Fact]
        public void CarrierUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = CarrierMock.Null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);
            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity });
        }

        [Fact]
        public void CarrierUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = CarrierMock.Get(Fake.GetKey());
            updateEntity.Name = null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity });
        }

        [Fact]
        public void CarrierUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = CarrierMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = CarrierMock.Get(Fake.GetKey());
            updateEntity.Name = string.Empty;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Carrier>().ToList();
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity });
        }

        private void Update(Carrier entity)
        {
            var service = new CarrierService(MockRepository.GetContext());
            service.Update(entity).Wait();
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
            result.Should().BeEquivalentToModel(entity);
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
            var result = Get(Fake.GetId(key));

            // assertation
            result.Should().BeNull();
        }

        private Carrier Get(Guid id)
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
            result.Should().BeEquivalentToModel(new List<Carrier>() { entity });
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

        private List<Carrier> GetAll()
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
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity });
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
            entities.Should().BeEquivalentToModel(new List<Carrier>() { entity2 });

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
using FluentAssertions;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Core.Exceptions;
using Insurance.Domain.Common;
using Insurance.Domain.Entities;
using Insurance.Domain.Services;
using Insurance.Infra.Data;
using Insurance.Test.Common;
using Insurance.Test.Mocks;
using Insurance.Test.Mocks.Domain.Entities;
using Insurance.Test.Mocks.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.Test.ComponentTests
{
    public class AdvisorAppServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;

        public AdvisorAppServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();
        }

        #region Add

        [Fact]
        public void AdvisorAddEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key);

            // act
            Add(model);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entityExpected.HealthStatus = entities[0].HealthStatus;
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorAddWithInputNull()
        {
            // arrange
            var model = AdvisorInputModelMock.Null;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisorAddEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key);
            model.Name = null;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisorAddEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key);
            model.Name = string.Empty;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisorAddEntityWithoutLastName()
        {
            // arrange
            var key = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key);
            model.LastName = null;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.LastNameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisorAddEntityWithLastNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key);
            model.LastName = string.Empty;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.LastNameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        private void Add(AdvisorInputModel model)
        {
            var service = new AdvisorAppService(MockRepository.GetContext());
            service.Add(model).Wait();
        }

        #endregion Add

        #region Update

        [Fact]
        public void AdvisorUpdateEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);

            // act
            Update(entity.Id, model);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var model = AdvisorInputModelMock.Null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorUpdateWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);

            // act
            Action action = () => Update(Fake.GetId(key), model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
        }

        [Fact]
        public void AdvisorUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);
            model.Name = null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);
            model.Name = string.Empty;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorUpdateEntityWithoutLastNamee()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);
            model.LastName = null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.LastNameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorUpdateEntityWithLastNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = AdvisorInputModelMock.Get(key2);
            model.LastName = string.Empty;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.LastNameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        private void Update(Guid id, AdvisorInputModel model)
        {
            var service = new AdvisorAppService(MockRepository.GetContext());
            service.Update(id, model).Wait();
        }

        #endregion Update

        #region Get

        [Fact]
        public void AdvisorGetEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = Get(entity.Id);

            // assertation
            var viewModelExpected = AdvisorViewModelMock.Get(key);
            result.Should().BeEquivalentTo(viewModelExpected);
        }

        [Fact]
        public void AdvisorGetEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Get(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);
        }

        [Fact]
        public void AdvisorGetEntityWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            // act
            Action action = () => Get(Fake.GetId(key));

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
        }

        private AdvisorViewModel Get(Guid id)
        {
            var service = new AdvisorAppService(MockRepository.GetContext());
            return service.Get(id).GetAwaiter().GetResult();
        }

        #endregion Get

        #region GetAll

        [Fact]
        public void AdvisorGetAllEntities()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = GetAll();

            // assertation
            var viewModelExpected = AdvisorViewModelMock.Get(key);
            result.Should().BeEquivalentTo(new List<AdvisorViewModel>() { viewModelExpected });
        }

        [Fact]
        public void AdvisorGetAllWithoutEntities()
        {
            // arrange

            // act
            var result = GetAll();

            // assertation
            result.Should().BeEmpty();
        }

        private List<AdvisorViewModel> GetAll()
        {
            var service = new AdvisorAppService(MockRepository.GetContext());
            return service.GetAll().GetAwaiter().GetResult();
        }

        #endregion GetAll

        #region Delete

        [Fact]
        public void AdvisorDeleteEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisoDeleteEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Delete(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });
        }

        [Fact]
        public void AdvisorDeleteEntityWithoutEntity()
        {
            // arrange

            // act
            Action action = () => Delete(Guid.NewGuid());

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisorDeleteEntityWithContract()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            var key2 = Fake.GetKey();

            var entity2 = AdvisorMock.Get(key2);
            MockRepository.Add(entity2);

            var contract = ContractMock.Get(entity.Id, entity2.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            var entityExpected = AdvisorMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Advisor>() { entityExpected });

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        private void Delete(Guid id)
        {
            var service = new AdvisorAppService(MockRepository.GetContext());
            service.Delete(id).Wait();
        }

        #endregion Delete
    }
}
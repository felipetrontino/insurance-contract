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
    public class AdvisorServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;

        public AdvisorServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();
        }

        #region Add

        [Fact]
        public void AdvisorAddEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);

            // act
            Add(entity);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity });
        }

        [Fact]
        public void AdvisorAddWithInputNull()
        {
            // arrange
            var entity = AdvisorMock.Null;

            // act
            Action action = () => Add(entity);

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

            var entity = AdvisorMock.Get(key);
            entity.Name = null;

            // act
            Action action = () => Add(entity);

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

            var entity = AdvisorMock.Get(key);
            entity.Name = string.Empty;

            // act
            Action action = () => Add(entity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEmpty();
        }

        private void Add(Advisor entity)
        {
            var service = new AdvisorService(MockRepository.GetContext());
            service.Add(entity).Wait();
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

            var updateEntity = AdvisorMock.Get(Fake.GetKey());
            updateEntity.Id = entity.Id;

            // act
            Update(updateEntity);

            // assertation
            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEquivalentToModel(new List<Advisor>() { updateEntity });
        }

        [Fact]
        public void AdvisorUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = AdvisorMock.Null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);
            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity });
        }

        [Fact]
        public void AdvisorUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = AdvisorMock.Get(Fake.GetKey());
            updateEntity.Name = null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity });
        }

        [Fact]
        public void AdvisorUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = AdvisorMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = AdvisorMock.Get(Fake.GetKey());
            updateEntity.Name = string.Empty;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Advisor>().ToList();
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity });
        }

        private void Update(Advisor entity)
        {
            var service = new AdvisorService(MockRepository.GetContext());
            service.Update(entity).Wait();
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
            result.Should().BeEquivalentToModel(entity);
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
            var result = Get(Fake.GetId(key));

            // assertation
            result.Should().BeNull();
        }

        private Advisor Get(Guid id)
        {
            var service = new AdvisorService(MockRepository.GetContext());
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
            result.Should().BeEquivalentToModel(new List<Advisor>() { entity });
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

        private List<Advisor> GetAll()
        {
            var service = new AdvisorService(MockRepository.GetContext());
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
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity });
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
            entities.Should().BeEquivalentToModel(new List<Advisor>() { entity2 });

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        private void Delete(Guid id)
        {
            var service = new AdvisorService(MockRepository.GetContext());
            service.Delete(id).Wait();
        }

        #endregion Delete
    }
}
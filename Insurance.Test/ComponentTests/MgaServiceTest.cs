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
    public class MgaServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;

        public MgaServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();
        }

        #region Add

        [Fact]
        public void MgaAddEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);

            // act
            Add(entity);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        [Fact]
        public void MgaAddWithInputNull()
        {
            // arrange
            var entity = MgaMock.Null;

            // act
            Action action = () => Add(entity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void MgaAddEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            entity.Name = null;

            // act
            Action action = () => Add(entity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void MgaAddEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            entity.Name = string.Empty;

            // act
            Action action = () => Add(entity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
        }

        private void Add(Mga entity)
        {
            var service = new MgaService(MockRepository.GetContext());
            service.Add(entity).Wait();
        }

        #endregion Add

        #region Update

        [Fact]
        public void MgaUpdateEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = MgaMock.Get(Fake.GetKey());
            updateEntity.Id = entity.Id;

            // act
            Update(updateEntity);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { updateEntity });
        }

        [Fact]
        public void MgaUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = MgaMock.Null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);
            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        [Fact]
        public void MgaUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = MgaMock.Get(Fake.GetKey());
            updateEntity.Name = null;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        [Fact]
        public void MgaUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var updateEntity = MgaMock.Get(Fake.GetKey());
            updateEntity.Name = string.Empty;

            // act
            Action action = () => Update(updateEntity);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        private void Update(Mga entity)
        {
            var service = new MgaService(MockRepository.GetContext());
            service.Update(entity).Wait();
        }

        #endregion Update

        #region Get

        [Fact]
        public void MgaGetEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = Get(entity.Id);

            // assertation
            result.Should().BeEquivalentToModel(entity);
        }

        [Fact]
        public void MgaGetEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Get(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);
        }

        [Fact]
        public void MgaGetEntityWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            // act
            var result = Get(Fake.GetId(key));

            // assertation
            result.Should().BeNull();
        }

        private Mga Get(Guid id)
        {
            var service = new MgaService(MockRepository.GetContext());
            return service.Get(id).GetAwaiter().GetResult();
        }

        #endregion Get

        #region GetAll

        [Fact]
        public void MgaGetAllEntities()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            var result = GetAll();

            // assertation
            result.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        [Fact]
        public void MgaGetAllWithoutEntities()
        {
            // arrange

            // act
            var result = GetAll();

            // assertation
            result.Should().BeEmpty();
        }

        private List<Mga> GetAll()
        {
            var service = new MgaService(MockRepository.GetContext());
            return service.GetAll().GetAwaiter().GetResult();
        }

        #endregion GetAll

        #region Delete

        [Fact]
        public void MgaDeleteEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void AdvisoDeleteEntityWithIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            // act
            Action action = () => Delete(Guid.Empty);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity });
        }

        [Fact]
        public void MgaDeleteEntityWithoutEntity()
        {
            // arrange

            // act
            Action action = () => Delete(Guid.NewGuid());

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
        }

        [Fact]
        public void MgaDeleteEntityWithContract()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            var key2 = Fake.GetKey();

            var entity2 = MgaMock.Get(key2);
            MockRepository.Add(entity2);

            var contract = ContractMock.Get(entity.Id, entity2.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            Delete(entity.Id);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEquivalentToModel(new List<Mga>() { entity2 });

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        private void Delete(Guid id)
        {
            var service = new MgaService(MockRepository.GetContext());
            service.Delete(id).Wait();
        }

        #endregion Delete
    }
}
using FluentAssertions;
using Insurance.Application.Interfaces;
using Insurance.Application.Models.InputModel;
using Insurance.Application.Models.ViewModel;
using Insurance.Core.Exceptions;
using Insurance.Domain.Common;
using Insurance.Domain.Entities;
using Insurance.Infra.CrossCutting;
using Insurance.Infra.Data;
using Insurance.Test.Common;
using Insurance.Test.Mocks;
using Insurance.Test.Mocks.Domain.Entities;
using Insurance.Test.Mocks.Models.InputModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.Test.ComponentTests
{
    public class MgaAppServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;
        protected readonly IMgaAppService Service;

        public MgaAppServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();

            var provider = DependencyInjectorStub.Get((s, c) =>
            {
                BootStrapper.RegisterServices(s, c);
                s.AddScoped(x => MockRepository.GetContext());
            });

            Service = provider.GetService<IMgaAppService>();
        }

        #region Add

        [Fact]
        public void MgaAddEntityValid()
        {
            // arrange
            var key = Fake.GetKey();

            var model = MgaInputModelMock.Get(key);

            // act
            Add(model);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            var entityExpected = MgaMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
        }

        [Fact]
        public void MgaAddWithInputNull()
        {
            // arrange
            var model = MgaInputModelMock.Null;

            // act
            Action action = () => Add(model);

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

            var model = MgaInputModelMock.Get(key);
            model.Name = null;

            // act
            Action action = () => Add(model);

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

            var model = MgaInputModelMock.Get(key);
            model.Name = string.Empty;

            // act
            Action action = () => Add(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            entities.Should().BeEmpty();
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

            var key2 = Fake.GetKey();
            var model = MgaInputModelMock.Get(key2);

            // act
            Update(entity.Id, model);

            // assertation
            var entities = MockRepository.Query<Mga>().ToList();
            var entityExpected = MgaMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
        }

        [Fact]
        public void MgaUpdateWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var model = MgaInputModelMock.Null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            var entityExpected = MgaMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
        }

        [Fact]
        public void MgaUpdateWithoutEntity()
        {
            // arrange
            var key = Fake.GetKey();

            var key2 = Fake.GetKey();

            var model = MgaInputModelMock.Get(key2);

            // act
            Action action = () => Update(Fake.GetId(key), model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
        }

        [Fact]
        public void MgaUpdateEntityWithoutName()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = MgaInputModelMock.Get(key2);
            model.Name = null;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            var entityExpected = MgaMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
        }

        [Fact]
        public void MgaUpdateEntityWithNameEmpty()
        {
            // arrange
            var key = Fake.GetKey();

            var entity = MgaMock.Get(key);
            MockRepository.Add(entity);

            MockRepository.Commit();

            var key2 = Fake.GetKey();

            var model = MgaInputModelMock.Get(key2);
            model.Name = string.Empty;

            // act
            Action action = () => Update(entity.Id, model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.NameInvalid);

            var entities = MockRepository.Query<Mga>().ToList();
            var entityExpected = MgaMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
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
            var viewModelExpected = MgaViewModelMock.Get(key);
            result.Should().BeEquivalentTo(viewModelExpected);
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
            Action action = () => Get(Fake.GetId(key));

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.EntityNotFound);
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
            var viewModelExpected = MgaViewModelMock.Get(key);
            result.Should().BeEquivalentTo(new List<MgaViewModel>() { viewModelExpected });
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
            var entityExpected = MgaMock.Get(key);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });
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
            var entityExpected = MgaMock.Get(key2);
            entities.Should().BeEquivalentToEntity(new List<Mga>() { entityExpected });

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        #endregion Delete

        private void Add(MgaInputModel model)
        {
            Service.Add(model).Wait();
        }

        private void Update(Guid id, MgaInputModel model)
        {
            Service.Update(id, model).Wait();
        }

        private MgaViewModel Get(Guid id)
        {
            return Service.Get(id).GetAwaiter().GetResult();
        }

        private List<MgaViewModel> GetAll()
        {
            return Service.GetAll().GetAwaiter().GetResult();
        }

        private void Delete(Guid id)
        {
            Service.Delete(id).Wait();
        }
    }
}
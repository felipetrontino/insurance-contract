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
using Insurance.Test.Mocks.Models.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.Test.ComponentTests
{
    public class ContractAppServiceTest
    {
        protected readonly IMockRepository<InsuranceDb> MockRepository;
        protected readonly IContractAppService Service;

        public ContractAppServiceTest()
        {
            MockRepository = new DbMockRepository<InsuranceDb>();

            var provider = DependencyInjectorStub.Get((s, c) =>
            {
                BootStrapper.RegisterServices(s, c);
                s.AddScoped(x => MockRepository.GetContext());
            });

            Service = provider.GetService<IContractAppService>();
        }

        #region Establish

        [Fact]
        public void ContractEstablishTwoEntityValid()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Establish(model);

            // assertation
            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractEstablishWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Null;

            // act
            Action action = () => Establish(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        [Fact]
        public void ContractEstablishWithFromIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(Guid.Empty, mga.Id);

            // act
            Action action = () => Establish(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        [Fact]
        public void ContractEstablishWithToIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, Guid.Empty);

            // act
            Action action = () => Establish(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        [Fact]
        public void ContractEstablishWithIdEquals()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, carrier.Id);

            // act
            Action action = () => Establish(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        [Fact]
        public void ContractEstablishDuplicate()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Action action = () => Establish(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractExists);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractEstablishWithContractFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            contract.Finished = true;

            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Establish(model);

            // assertation
            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected1 = ContractMock.Get(carrier.Id, mga.Id);
            contractExpected1.Finished = true;
            var contractExpected2 = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected1, contractExpected2 });
        }

        #endregion Establish

        #region Terminate

        [Fact]
        public void ContractTerminateTwoEntityValid()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Terminate(model);

            // assertation
            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contractExpected.Finished = true;
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractTerminateWithFInputNull()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Null;

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractTerminateWithFromIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(Guid.Empty, mga.Id);

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractTerminateWithToIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, Guid.Empty);

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractTerminateWithIdEquals()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, carrier.Id);

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractInvalid);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        [Fact]
        public void ContractTerminateContractNotExists()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractNotExists);

            var contracts = MockRepository.Query<Contract>().ToList();
            contracts.Should().BeEmpty();
        }

        [Fact]
        public void ContractTerminateWithContractFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            contract.Finished = true;
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            Action action = () => Terminate(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractFinished);

            var contracts = MockRepository.Query<Contract>().ToList();
            var contractExpected = ContractMock.Get(carrier.Id, mga.Id);
            contractExpected.Finished = true;
            contracts.Should().BeEquivalentToEntity(new List<Contract>() { contractExpected });
        }

        #endregion Terminate

        #region FindShortestPath

        [Fact]
        public void ContractFindShortestPathTwoEntityValid()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            var carrierViewModelExpected1 = ContractPartViewModelMock.Get(key);
            var carrierViewModelExpected2 = ContractPartViewModelMock.Get(key2);
            result.Should().BeEquivalentTo(new List<ContractPartViewModel>() { carrierViewModelExpected1, carrierViewModelExpected2 });
        }

        [Fact]
        public void ContractFindShortestPathWithInputNull()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Null;

            // act
            Action action = () => FindShortestPath(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.InputInvalid);
        }

        [Fact]
        public void ContractFindShortestPathWithFromIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(Guid.Empty, mga.Id);

            // act
            Action action = () => FindShortestPath(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);
        }

        [Fact]
        public void ContractFindShortestPathWithToIdEmpty()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, Guid.Empty);

            // act
            Action action = () => FindShortestPath(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.IdInvalid);
        }

        [Fact]
        public void ContractFindShortestPathWithIdEquals()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, carrier.Id);

            // act
            Action action = () => FindShortestPath(model);

            // assertation
            action.Should().Throw<ValidationBusinessException>().WithMessage(ValidationMessage.ContractInvalid);
        }

        [Fact]
        public void ContractFindShortestPathWithoutContracts()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractFindShortestPathWithoutContractParts()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractFindShortestPathTwoEntityWithContractFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            contract.Finished = true;

            MockRepository.Add(contract);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, mga.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractFindShortestPathThreeEntityValid()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(mga.Id, advisor.Id);
            MockRepository.Add(contract2);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, advisor.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            var carrierViewModelExpected1 = ContractPartViewModelMock.Get(key);
            var carrierViewModelExpected2 = ContractPartViewModelMock.Get(key2);
            var carrierViewModelExpected3 = ContractPartViewModelMock.Get(key3);
            result.Should().BeEquivalentTo(new List<ContractPartViewModel>() { carrierViewModelExpected1, carrierViewModelExpected2, carrierViewModelExpected3 });
        }

        [Fact]
        public void ContractFindShortestPathThreeEntityWithContractToFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);
            contract1.Finished = true;
            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(mga.Id, advisor.Id);
            MockRepository.Add(contract2);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, advisor.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractFindShortestPathThreeEntityWithContractFromFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(mga.Id, advisor.Id);
            contract2.Finished = true;
            MockRepository.Add(contract2);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, advisor.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractFindShortestPathEntityWithTwoContracts()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(carrier.Id, advisor.Id);
            MockRepository.Add(contract2);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, advisor.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            var carrierViewModelExpected1 = ContractPartViewModelMock.Get(key);
            var carrierViewModelExpected3 = ContractPartViewModelMock.Get(key3);
            result.Should().BeEquivalentTo(new List<ContractPartViewModel>() { carrierViewModelExpected1, carrierViewModelExpected3 });
        }

        [Fact]
        public void ContractFindShortestPathComplex()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(mga.Id, advisor.Id);
            MockRepository.Add(contract2);

            var contract3 = ContractMock.Get(carrier.Id, advisor.Id);
            MockRepository.Add(contract3);

            MockRepository.Commit();

            var model = ContractInputModelMock.Get(carrier.Id, advisor.Id);

            // act
            var result = FindShortestPath(model);

            // assertation
            var carrierViewModelExpected1 = ContractPartViewModelMock.Get(key);
            var carrierViewModelExpected3 = ContractPartViewModelMock.Get(key3);
            result.Should().BeEquivalentTo(new List<ContractPartViewModel>() { carrierViewModelExpected1, carrierViewModelExpected3 });
        }

        #endregion FindShortestPath

        #region GetAll

        [Fact]
        public void ContractGetAllEntities()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            var result = GetAll();

            // assertation
            var contractViewModelExpected = ContractViewModelMock.Get(carrier, mga);
            result.Should().BeEquivalentTo(new List<ContractViewModel>() { contractViewModelExpected });
        }

        [Fact]
        public void ContractGetAllWithoutEntities()
        {
            // arrange

            // act
            var result = GetAll();

            // assertation
            result.Should().BeEmpty();
        }

        #endregion GetAll

        #region GetParts

        [Fact]
        public void ContractGetPartsEntities()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            MockRepository.Commit();

            // act
            var result = GetParts();

            // assertation
            var contractPartViewModelExpected = ContractPartViewModelMock.Get(key);
            result.Should().BeEquivalentTo(new List<ContractPartViewModel>() { contractPartViewModelExpected });
        }

        [Fact]
        public void ContractGetPartsWithoutEntities()
        {
            // arrange

            // act
            var result = GetParts();

            // assertation
            result.Should().BeEmpty();
        }

        #endregion GetParts

        #region GetNodes

        [Fact]
        public void ContractGetNodesEntities()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);

            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            var result = GetNodes();

            // assertation
            var nodeViewModelExpected1 = NodeViewModelMock.Get(key);
            var nodeViewModelExpected2 = NodeViewModelMock.Get(key2);
            result.Should().BeEquivalentTo(new List<NodeViewModel>() { nodeViewModelExpected1, nodeViewModelExpected2 });
        }

        [Fact]
        public void ContractGetNodesWithoutEntities()
        {
            // arrange

            // act
            var result = GetNodes();

            // assertation
            result.Should().BeEmpty();
        }

        [Fact]
        public void ContractGetNodesWithoutContracts()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            MockRepository.Commit();

            // act
            var result = GetNodes();

            // assertation
            var nodeViewModelExpected1 = NodeViewModelMock.Get(key);
            var nodeViewModelExpected2 = NodeViewModelMock.Get(key2);
            result.Should().BeEquivalentTo(new List<NodeViewModel>() { nodeViewModelExpected1, nodeViewModelExpected2 });
        }

        [Fact]
        public void ContractGetNodesWithContractFinished()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            contract.Finished = true;

            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            var result = GetNodes();

            // assertation
            var nodeViewModelExpected1 = NodeViewModelMock.Get(key);
            var nodeViewModelExpected2 = NodeViewModelMock.Get(key2);
            result.Should().BeEquivalentTo(new List<NodeViewModel>() { nodeViewModelExpected1, nodeViewModelExpected2 });
        }

        [Fact]
        public void ContractGetNodesPartWithTwoContracts()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var key3 = Fake.GetKey();
            var advisor = AdvisorMock.Get(key3);

            MockRepository.Add(advisor);

            var contract1 = ContractMock.Get(carrier.Id, mga.Id);

            MockRepository.Add(contract1);

            var contract2 = ContractMock.Get(carrier.Id, advisor.Id);

            MockRepository.Add(contract2);

            MockRepository.Commit();

            // act
            var result = GetNodes();

            // assertation
            var nodeViewModelExpected1 = NodeViewModelMock.Get(key);
            var nodeViewModelExpected2 = NodeViewModelMock.Get(key2);
            var nodeViewModelExpected3 = NodeViewModelMock.Get(key3);
            result.Should().BeEquivalentTo(new List<NodeViewModel>() { nodeViewModelExpected1, nodeViewModelExpected2, nodeViewModelExpected3 });
        }

        #endregion GetNodes

        #region GetEdges

        [Fact]
        public void ContractGetEdgesEntities()
        {
            // arrange
            var key = Fake.GetKey();
            var carrier = CarrierMock.Get(key);

            MockRepository.Add(carrier);

            var key2 = Fake.GetKey();
            var mga = MgaMock.Get(key2);

            MockRepository.Add(mga);

            var contract = ContractMock.Get(carrier.Id, mga.Id);
            MockRepository.Add(contract);

            MockRepository.Commit();

            // act
            var result = GetEdges();

            // assertation
            var edgeViewModelExpected = EdgeViewModelMock.Get(carrier.Id, mga.Id);
            result.Should().BeEquivalentTo(new List<EdgeViewModel>() { edgeViewModelExpected });
        }

        [Fact]
        public void ContractGetEdgesWithoutEntities()
        {
            // arrange

            // act
            var result = GetEdges();

            // assertation
            result.Should().BeEmpty();
        }

        #endregion GetEdges

        private void Establish(ContractInputModel model)
        {
            Service.Establish(model).Wait();
        }

        private void Terminate(ContractInputModel model)
        {
            Service.Terminate(model).Wait();
        }

        private List<ContractPartViewModel> FindShortestPath(ContractInputModel model)
        {
            return Service.FindShortestPath(model).GetAwaiter().GetResult();
        }

        private List<ContractViewModel> GetAll()
        {
            return Service.GetAll().GetAwaiter().GetResult();
        }

        private List<ContractPartViewModel> GetParts()
        {
            return Service.GetParts().GetAwaiter().GetResult();
        }

        private List<NodeViewModel> GetNodes()
        {
            return Service.GetNodes().GetAwaiter().GetResult();
        }

        private List<EdgeViewModel> GetEdges()
        {
            return Service.GetEdges().GetAwaiter().GetResult();
        }
    }
}
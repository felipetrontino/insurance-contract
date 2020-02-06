import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from "@angular/router";
import { ContractInputModel, ContractViewModel, Part } from "../models/contract.model";
import { ContractService } from "../services/contract.service";
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-list-contract',
  templateUrl: './list-contract.component.html',
  styleUrls: ['./list-contract.component.css']
})
export class ListContractComponent implements OnInit {
  models$: Observable<ContractViewModel[]>;
  form: FormGroup;
  parts$: Observable<Part[]>;
  formFromId: string;
  formToId: string;
  result: string

  constructor(private router: Router, private service: ContractService, private formBuilder: FormBuilder) {
    this.formFromId = 'from';
    this.formToId = 'to';

    this.form = this.formBuilder.group(
      {
        from: [''],
        to: [''],        
      }
    )
  }

  ngOnInit() {
    this.loadModels();
  }

  loadModels() {
    this.models$ = this.service.getContracts();
    this.parts$ = this.service.getParts();
  }

  terminate(fromId, toId): void {
    const ans = confirm('Do you want to terminate contract with fromId: ' + fromId + 'and toId' + toId);
    if (ans) {
      let model: ContractInputModel = {
        fromId: fromId,
        toId: toId
      };

      this.service.terminateContract(model)
        .subscribe(data => {
          this.loadModels();
        })
    }
  };

  find(): void {
    let model: ContractInputModel = {
      fromId: this.form.get(this.formFromId).value,
      toId: this.form.get(this.formToId).value,
    };

    this.service.findShortestPath(model)
      .subscribe(data => {
        this.result = data.map(x => x.name).join(" => ");        
      });
  };
}

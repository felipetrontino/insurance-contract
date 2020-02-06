import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ContractService } from '../services/contract.service';
import { ContractInputModel, Part } from '../models/contract.model';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.css']
})
export class AddContractComponent implements OnInit {
  parts$: Observable<Part[]>; 
  form: FormGroup;
  formFromId: string;
  formToId: string;
  errorMessage: any;

  constructor(private contractService: ContractService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    this.formFromId = 'from';
    this.formToId = 'to';

    this.form = this.formBuilder.group(
      {
        from: ['', [Validators.required]],
        to: ['', [Validators.required]]
      }
    )
  }

  ngOnInit() {
    this.loadDropbox();
  }

  loadDropbox()
  {
    this.parts$ = this.contractService.getParts();    
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    let Model: ContractInputModel = {
      fromId: this.form.get(this.formFromId).value,
      toId: this.form.get(this.formToId).value,
    };

    this.contractService.establishContract(Model)
      .subscribe((data) => {
        this.router.navigate(['contract']);
      });
  }

  cancel() {
    this.router.navigate(['/contract']);
  }

  get from() { return this.form.get(this.formFromId); }
  get to() { return this.form.get(this.formToId); }

}

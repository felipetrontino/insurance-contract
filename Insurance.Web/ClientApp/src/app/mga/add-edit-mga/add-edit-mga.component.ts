import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MgaService } from '../services/mga.service';
import { Mga } from '../models/mga.model';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './add-edit-mga.component.html',
  styleUrls: ['./add-edit-mga.component.css']
})
export class AddEditMgaComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formName: string;
  formAddress: string;
  formPhone: string;
  modelId: string;
  errorMessage: any;
  existingMga: Mga;

  constructor(private mgaService: MgaService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formName = 'name';
    this.formAddress = 'address';
    this.formPhone = 'phone';

    if (this.avRoute.snapshot.params[idParam]) {
      this.modelId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        modelId: '',
        name: ['', [Validators.required]],
        address: '',
        phone: '',
      }
    )
  }

  ngOnInit() {
    if (this.modelId != undefined) {
      this.actionType = 'Edit';
      this.mgaService.getMga(this.modelId)
        .subscribe(data => (
          this.existingMga = data,
          this.form.controls[this.formName].setValue(data.name),
          this.form.controls[this.formAddress].setValue(data.address),
          this.form.controls[this.formPhone].setValue(data.phone)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let mga: Mga = {
        id: '00000000-0000-0000-0000-000000000000',
        name: this.form.get(this.formName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value
      };

      this.mgaService.addMga(mga)
        .subscribe((data) => {
          this.router.navigate(['mga']);
        });
    }

    if (this.actionType === 'Edit') {
      let mga: Mga = {
        id: this.existingMga.id,
        name: this.form.get(this.formName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value
      };
      this.mgaService.updateMga(mga.id, mga)
        .subscribe((data) => {
          this.router.navigate(['mga']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/mga']);
  }

  get name() { return this.form.get(this.formName); }
}

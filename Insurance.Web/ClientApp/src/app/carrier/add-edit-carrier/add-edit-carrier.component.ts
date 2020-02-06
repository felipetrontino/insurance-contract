import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CarrierService } from '../services/carrier.service';
import { Carrier } from '../models/carrier.model';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './add-edit-carrier.component.html',
  styleUrls: ['./add-edit-carrier.component.css']
})
export class AddEditCarrierComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formName: string;
  formAddress: string;
  formPhone: string;
  modelId: string;
  errorMessage: any;
  existingCarrier: Carrier;

  constructor(private carrierService: CarrierService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
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
      this.carrierService.getCarrier(this.modelId)
        .subscribe(data => (
          this.existingCarrier = data,
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
      let carrier: Carrier = {  
        id: '00000000-0000-0000-0000-000000000000',      
        name: this.form.get(this.formName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value
      };

      this.carrierService.addCarrier(carrier)
        .subscribe((data) => {
          this.router.navigate(['carrier']);
        });
    }

    if (this.actionType === 'Edit') {
      let carrier: Carrier = {
        id: this.existingCarrier.id,
        name: this.form.get(this.formName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value
      };
      this.carrierService.updateCarrier(carrier.id, carrier)
        .subscribe((data) => {
          this.router.navigate(['carrier']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/carrier']);
  }

  get name() { return this.form.get(this.formName); }
  
}

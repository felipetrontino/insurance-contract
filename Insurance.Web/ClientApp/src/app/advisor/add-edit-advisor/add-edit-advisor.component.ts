import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AdvisorService } from '../services/advisor.service';
import { Advisor, HealthStatus } from '../models/advisor.model';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './add-edit-advisor.component.html',
  styleUrls: ['./add-edit-advisor.component.css']
})
export class AddEditAdvisorComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formFirstName: string;
  formLastName: string;
  formAddress: string;
  formPhone: string;
  modelId: string;
  errorMessage: any;
  existingAdvisor: Advisor;

  constructor(private advisorService: AdvisorService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formFirstName = 'firstName';
    this.formLastName = 'lastName';
    this.formAddress = 'address';
    this.formPhone = 'phone';

    if (this.avRoute.snapshot.params[idParam]) {
      this.modelId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        modelId: '',
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        address: '',
        phone: '',
        heathStatus: 0,
      }
    )
  }

  ngOnInit() {
    if (this.modelId != undefined) {
      this.actionType = 'Edit';
      this.advisorService.getAdvisor(this.modelId)
        .subscribe(data => (
          this.existingAdvisor = data,
          this.form.controls[this.formFirstName].setValue(data.name),
          this.form.controls[this.formLastName].setValue(data.lastName),
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
      let advisor: Advisor = {
        id: '00000000-0000-0000-0000-000000000000',
        name: this.form.get(this.formFirstName).value,
        lastName: this.form.get(this.formLastName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value,
        healthStatus: this.randomize()
      };

      this.advisorService.addAdvisor(advisor)
        .subscribe((data) => {
          this.router.navigate(['advisor']);
        });
    }

    if (this.actionType === 'Edit') {
      let advisor: Advisor = {
        id: this.existingAdvisor.id,
        name: this.form.get(this.formFirstName).value,
        lastName: this.form.get(this.formLastName).value,
        address: this.form.get(this.formAddress).value,
        phone: this.form.get(this.formPhone).value,
        healthStatus: this.existingAdvisor.healthStatus
      };

      this.advisorService.updateAdvisor(advisor.id, advisor)
        .subscribe((data) => {
          this.router.navigate(['advisor']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/advisor']);
  }

  get firstName() { return this.form.get(this.formFirstName); }
  get lastName() { return this.form.get(this.formLastName); }

  randomize() : string {
    let min = 1;
    let max = 100;
    let value = Math.floor(Math.random()*(max-min+1)+min);
    return value >= 70 ? HealthStatus[HealthStatus.Green] : HealthStatus[HealthStatus.Red];    
  }
}

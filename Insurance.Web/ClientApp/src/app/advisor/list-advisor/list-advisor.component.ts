import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from "@angular/router";
import { Advisor, HealthStatus } from "../models/advisor.model";
import { AdvisorService } from "../services/advisor.service";

@Component({
  selector: 'app-list-advisor',
  templateUrl: './list-advisor.component.html',
  styleUrls: ['./list-advisor.component.css']
})
export class ListAdvisorComponent implements OnInit {
  models$: Observable<Advisor[]>;

  constructor(private router: Router, private service: AdvisorService) { }

  ngOnInit() {
    this.loadModels();
  }

  loadModels() {
        this.models$ = this.service.getAdvisors();
  }

  delete(id): void {
    const ans = confirm('Do you want to delete advisor with id: ' + id);
    if (ans) {
      this.service.deleteAdvisor(id)
        .subscribe(data => {
          this.loadModels();
        })
    }    
  }  

  getDescription(value)  {
    return HealthStatus[value];
  }
}

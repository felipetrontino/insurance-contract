import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from "@angular/router";
import { Carrier } from "../models/carrier.model";
import { CarrierService } from "../services/carrier.service";

@Component({
  selector: 'app-list-carrier',
  templateUrl: './list-carrier.component.html',
  styleUrls: ['./list-carrier.component.css']
})
export class ListCarrierComponent implements OnInit {
  models$: Observable<Carrier[]>;

  constructor(private router: Router, private service: CarrierService) { }

  ngOnInit() {
    this.loadModels();
  }

  loadModels() {
    this.models$ = this.service.getCarriers();
  }

  delete(id): void {
    const ans = confirm('Do you want to delete carrier with id: ' + id);
    if (ans) {
      this.service.deleteCarrier(id)
        .subscribe(data => {
          this.loadModels();
        })
    }
  };
}

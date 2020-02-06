import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from "@angular/router";
import { Mga } from "../models/mga.model";
import { MgaService } from "../services/mga.service";

@Component({
  selector: 'app-list-mga',
  templateUrl: './list-mga.component.html',
  styleUrls: ['./list-mga.component.css']
})
export class ListMgaComponent implements OnInit {
  models$: Observable<Mga[]>;

  constructor(private router: Router, private service: MgaService) { }

  ngOnInit() {
    this.loadModels();
  }

  loadModels() {
    this.models$ = this.service.getMgas();
  }

  delete(id): void {
    const ans = confirm('Do you want to delete mga with id: ' + id);
    if (ans) {
      this.service.deleteMga(id)
        .subscribe(data => {
          this.loadModels();
        })
    }
  };
}

import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from "@angular/router";
import { Node, Edge } from "../models/visualization.model";
import { VisualizationService } from "../services/visualization.service";

@Component({
  selector: 'app-list-all',
  templateUrl: './list-all.component.html',
  styleUrls: ['./list-all.component.css']
})
export class ListAllComponent implements OnInit { 
  nodes: any[];
  edges: any[];  

  constructor(private router: Router, private service: VisualizationService) { }

  ngOnInit() {
    this.nodes = [];
    this.edges = [];
    
    this.loadModels();
  }

  loadModels() {   

    this.service.getNodes().subscribe(items => {
      for (var i = 0; i < items.length; i++) {
        let item = items[i];

        this.nodes.push({
          id: item.id,
          label: item.name,
        });
      }    
    }); 
    
    this.service.getEdges().subscribe(items => {
      for (var i = 0; i < items.length; i++) {
        let item = items[i];

        this.edges.push({
          id: i,
          source: item.fromId,
          target: item.toId,
          label: 'contract',
        });
      }    
    });
  }  
}

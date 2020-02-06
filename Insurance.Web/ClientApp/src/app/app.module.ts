import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxGraphModule } from '@swimlane/ngx-graph';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ListAdvisorComponent } from './advisor/list-advisor/list-advisor.component';
import { AddEditAdvisorComponent } from './advisor/add-edit-advisor/add-edit-advisor.component';
import { ListCarrierComponent } from './carrier/list-carrier/list-carrier.component';
import { AddEditCarrierComponent } from './carrier/add-edit-carrier/add-edit-carrier.component';
import { ListMgaComponent } from './mga/list-mga/list-mga.component';
import { AddEditMgaComponent } from './mga/add-edit-mga/add-edit-mga.component';
import { ListContractComponent } from './contract/list-contract/list-contract.component';
import { AddContractComponent } from './contract/add-contract/add-contract.component';
import { ListAllComponent } from './visualization/list-all/list-all.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ListAdvisorComponent,
    AddEditAdvisorComponent,
    ListCarrierComponent,
    AddEditCarrierComponent,
    ListMgaComponent,
    AddEditMgaComponent,
    ListContractComponent,
    AddContractComponent,
    ListAllComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxGraphModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'advisor', component: ListAdvisorComponent },
      { path: 'advisor/add', component: AddEditAdvisorComponent },
      { path: 'advisor/edit/:id', component: AddEditAdvisorComponent },
      { path: 'carrier', component: ListCarrierComponent },
      { path: 'carrier/add', component: AddEditCarrierComponent },
      { path: 'carrier/edit/:id', component: AddEditCarrierComponent },
      { path: 'mga', component: ListMgaComponent },
      { path: 'mga/add', component: AddEditMgaComponent },
      { path: 'mga/edit/:id', component: AddEditMgaComponent },
      { path: 'contract', component: ListContractComponent },
      { path: 'contract/establish', component: AddContractComponent },
      { path: 'visualization', component: ListAllComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {HttpClientModule} from '@angular/common/http';
import { InvoiceListComponent } from './invoice-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { AppMaterialModule } from "./app.material-module";
import { NewInvoiceDialogeComponent } from './new-invoice-dialoge/new-invoice-dialoge.component';
import {MatDialogModule} from '@angular/material/dialog';
@NgModule({
  declarations: [
    AppComponent,
    InvoiceListComponent,
    NewInvoiceDialogeComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule
  ],
  exports: [
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

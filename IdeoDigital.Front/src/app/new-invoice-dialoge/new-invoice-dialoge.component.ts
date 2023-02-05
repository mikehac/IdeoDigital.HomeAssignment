import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Invoice, InvoiceService } from '../invoice-service'
import { ItemListComponent } from '../item-list/item-list.component'
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-new-invoice-dialoge',
  templateUrl: './new-invoice-dialoge.component.html',
  styleUrls: ['./new-invoice-dialoge.component.css']
})
export class NewInvoiceDialogeComponent implements OnInit {
  form: FormGroup;
  items: any[];

  constructor(
    private formBuilder: FormBuilder,
    private invoiceService: InvoiceService,
    private dialogRef: MatDialogRef<NewInvoiceDialogeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      supplierName: this.formBuilder.control(''),
      supplierAddress: this.formBuilder.control(''),
      customerName: this.formBuilder.control(''),
      customerAddress: this.formBuilder.control(''),
      date: this.formBuilder.control(''),
      dueDate: this.formBuilder.control('')
    });
  }

  public onSubmit(newInvoice) {
    console.log(newInvoice);
    var invoiceToSave = {
      customerName: newInvoice.customerName,
      customerAddress: newInvoice.customerAddress,
      supplierName: newInvoice.supplierName,
      supplierAddress: newInvoice.supplierAddress,
      date: newInvoice.date,
      dueDate: newInvoice.dueDate,
      subTotal: 0.00,
      invoiceStatus: 'newInvoice.invoiceStatus',
      items: this.items
    };
    this.invoiceService.post('invoice', invoiceToSave)
      .subscribe(response => {
        console.log(response);
        //close the popup
        this.dialogRef.close();
      });
  }
  onItemAdded(items) {
    console.log(items);
    this.items = items;
  }
}
export interface DialogData {
  action: string
}
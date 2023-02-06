import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Invoice, InvoiceService } from '../invoice-service'
import { Item } from '../item-list/item-list.component'
import { MatDialogRef } from '@angular/material/dialog';
import { formatDate } from '@angular/common' 

@Component({
  selector: 'app-new-invoice-dialoge',
  templateUrl: './new-invoice-dialoge.component.html',
  styleUrls: ['./new-invoice-dialoge.component.css']
})
export class NewInvoiceDialogeComponent implements OnInit {
  form: FormGroup;
  items: Item[];
  defaultSelected = "3";
  buttonText: string = 'Save';
  statuses: Status[] = [
    { value: 1, text: 'Payed' },
    { value: 2, text: 'Partially Payed' },
    { value: 3, text: 'Not Payed' }
  ];

  constructor(
    private formBuilder: FormBuilder,
    private invoiceService: InvoiceService,
    private dialogRef: MatDialogRef<NewInvoiceDialogeComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
      this.form = this.formBuilder.group({
        supplierName: this.formBuilder.control(''),
        supplierAddress: this.formBuilder.control(''),
        customerName: this.formBuilder.control(''),
        customerAddress: this.formBuilder.control(''),
        date: this.formBuilder.control(''),
        dueDate: this.formBuilder.control('')
      });
      this.form.controls['date'].setValue(formatDate(new Date(), 'yyyy-MM-dd', 'en'));
      this.form.controls['dueDate'].setValue(formatDate(new Date(), 'yyyy-MM-dd', 'en'));
  }

  ngOnInit(): void {
    if (this.data.action === 'invoiceDetails') {
      this.buttonText = 'Update';
      this.invoiceService.getById('invoice', this.data.id)
        .subscribe(response => {
          this.loadInvoiceDetails(response);
        });
    }
  }

  private loadInvoiceDetails(invoice) {
    this.form.controls['supplierName'].setValue(invoice.supplierName);
    this.form.controls['supplierAddress'].setValue(invoice.supplierAddress);
    this.form.controls['customerName'].setValue(invoice.customerName);
    this.form.controls['customerAddress'].setValue(invoice.customerAddress);
    this.form.controls['date'].setValue(formatDate(invoice.date, 'yyyy-MM-dd', 'en'));
    this.form.controls['dueDate'].setValue(formatDate(invoice.dueDate, 'yyyy-MM-dd', 'en'));
    this.items = invoice.items;
  }
  
  public onSubmit(newInvoice) {
    console.log(newInvoice);
    let currentInvoice = this.statuses?.find(x => x.value == parseInt(this.defaultSelected))?.text ?? 'Not Payed';
    var invoiceToSave = {
      id: 0,
      customerName: newInvoice.customerName,
      customerAddress: newInvoice.customerAddress,
      supplierName: newInvoice.supplierName,
      supplierAddress: newInvoice.supplierAddress,
      date: newInvoice.date,
      dueDate: newInvoice.dueDate,
      subTotal: 0.00,
      invoiceStatus: currentInvoice,
      items: this.items,
      statusId: parseInt(this.defaultSelected)
    };
    if (this.data.action === 'invoiceDetails') {
      invoiceToSave.id = this.data.id;
      this.invoiceService.put('invoice', invoiceToSave)
      .subscribe(response => {
        console.log(response);
        this.dialogRef.close();
      });
    }
    else {
      this.invoiceService.post('invoice', invoiceToSave)
      .subscribe(response => {
        console.log(response);
        this.dialogRef.close();
      });
    }
  }
  onItemAdded(items) {
    console.log(items);
    this.items = items;
  }
}
export interface DialogData {
  action: string,
  id: number
}

interface Status {
  value: number,
  text: string
}
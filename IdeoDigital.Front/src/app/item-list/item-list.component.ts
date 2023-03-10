import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  @Input() Items: Item[];
  @Input() InvoiceId: number;
  @Output() addingItems = new EventEmitter<Item[]>();
  public displayedColumns = ['description', 'quentity', 'rate','isEdit'];
  public dataSource = new MatTableDataSource<Item>();
  constructor() { }

  ngOnInit(): void {
    this.dataSource.data.push({
        invoiceId: 0,
        description: '',
        quentity: 0,
        rate: 0,
        isEdit: true
      });    
  }
  ngDoCheck(): void{
    if (this.Items != null) {
      this.dataSource.data = this.Items;
    }
  }
  addRow(event) {
    event.preventDefault();
    const newRow = {
      invoiceId: 0,
      description: '',
      quentity: 0,
      rate: 0,
      isEdit: true
    };
    if (this.InvoiceId != undefined) {
      newRow.invoiceId = this.InvoiceId;
      this.Items = [...this.Items, newRow];
      this.dataSource.data = this.Items;
    }
    else {
      this.dataSource.data = [...this.dataSource.data, newRow];      
    }
  }
  removeRow(element) {
    console.log(element);
    const index = this.dataSource.data.indexOf(element.id);
    this.dataSource.data.splice(index, 1);
    this.dataSource._updateChangeSubscription();
  }
  onFocusOut() {
    this.addingItems.emit(this.dataSource.data);
  }
}

export interface Item{
  invoiceId:number,
  description: string,
  quentity: number,
  rate: number,
  isEdit: boolean  
}

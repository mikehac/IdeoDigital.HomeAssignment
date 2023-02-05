import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.css']
})
export class ItemListComponent implements OnInit {
  @Output() addingItems = new EventEmitter<Item[]>();
  public displayedColumns = ['description', 'quentity', 'rate'];
  public dataSource = new MatTableDataSource<Item>();
  constructor() { }

  ngOnInit(): void {
    this.dataSource.data.push({
      description: '',
      quentity: 0,
      rate: 0,
      isEdit: true
    });
  }
  addRow() {
    const newRow = {
      description: '',
      quentity: 0,
      rate: 0,
      isEdit: true
    };
    this.dataSource.data = [...this.dataSource.data, newRow];
  }
  removeRow(element) {
    console.log(element);
  }
  onFocusOut() {
    this.addingItems.emit(this.dataSource.data);
  }
}

export interface Item{
  description: string,
  quentity: number,
  rate: number,
  isEdit: boolean  
}

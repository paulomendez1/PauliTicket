import { Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { OrderDTO } from 'src/app/models/order';
import { AuthService } from 'src/app/services/auth.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  public username!: string;
  public email!: string;
  public displayedColumns: string[] = ['eventName', 'orderPlaced', 'orderTotal'];
  public orders!: OrderDTO[];
  public totalAmountOfRecords: any;
  public currentPage = 1;
  public pageSize = 5;

  constructor(private authService: AuthService, private orderService: OrderService) { }

  ngOnInit() {
    this.loadData();
    this.email = this.authService.getFieldFromJWT('email');
    this.username = this.authService.getFieldFromJWT('sub');
  }

  loadData() {
    var id = this.authService.getFieldFromJWT('uid');
    this.orderService.getPagedOrders(id, this.currentPage, this.pageSize).subscribe(response => {
      this.orders = response.ordersForMonth;
      this.totalAmountOfRecords = response.count;
    })
  }

  updatePagination(event: PageEvent) {
    this.currentPage = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadData();
  }
}

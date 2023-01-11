import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EventDTO } from 'src/app/models/event';
import { OrderDTO } from 'src/app/models/order';
import { AuthService } from 'src/app/services/auth.service';
import { EventService } from 'src/app/services/event.service';
import { OrderService } from 'src/app/services/order.service';
import { OrderDialogComponent } from '../order-dialog/order-dialog.component';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute,
    private orderService: OrderService,
    private eventService: EventService,
    public dialog: MatDialog,
    private authService: AuthService,
    private router: Router) { }

  public event!: EventDTO;
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.eventService.getEvent(params['id']).subscribe((event) => {
        this.event = event;
      });
    })
  }
  openDialog(): void {
    const dialogRef = this.dialog.open(OrderDialogComponent, {
      data: { name: this.event.name, price: `$${this.event.price}` },
      width: '500px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.createOrder(result);
    });
  }

  public createOrder(order: any) {
    var userId = this.authService.getFieldFromJWT('uid');

    var orderDTO = {
      userId: userId,
      orderTotal: this.event.price,
      orderPaid: true,
      eventId: this.event.eventId
    } as OrderDTO;
    this.orderService.createOrder(orderDTO).subscribe(id => {
      this.router.navigate([`/order/${id}`]);
    })
  }
}

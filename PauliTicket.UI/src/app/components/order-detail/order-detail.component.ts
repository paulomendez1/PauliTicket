import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventDTO } from 'src/app/models/event';
import { OrderDTO } from 'src/app/models/order';
import { EventService } from 'src/app/services/event.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {

  public order!: OrderDTO;
  public event!: EventDTO;

  constructor(private activatedRoute: ActivatedRoute, private orderService: OrderService, private eventService: EventService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.orderService.getOrder(params['id']).subscribe((order) => {
        this.order = order;
        this.eventService.getEvent(this.order.eventId).subscribe((event) => {
          this.event = event;
        });
      });
    })
  }



}

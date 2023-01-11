import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, retry, timeout } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { EventDTO } from '../models/event';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  EVENT_URL = environment.apiURL + '/event';

  constructor(private http: HttpClient) { }

  public getEvent(eventId: string): Observable<EventDTO> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    return this.http.get<any>(`${this.EVENT_URL}/${eventId}`, { headers: headers }).pipe(
      retry(3),
      timeout(15000),
      map(data => data as EventDTO)
    );
  }

  public getEventsForCategories(categoriesName: string[]): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    let params = new HttpParams();
    for (const categoryName of categoriesName) {
      params = params.append('categories', categoryName);
    }
    return this.http.get<any>(`${this.EVENT_URL}/EventsForCategories`, { headers: headers, params: params }).pipe(
      retry(3),
      timeout(15000),
      map(data => data as Event[])
    );
  }
}

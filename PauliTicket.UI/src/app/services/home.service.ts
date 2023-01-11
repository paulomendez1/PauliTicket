import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, retry, timeout } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { CategoryDTO } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  CATEGORY_URL = environment.apiURL + '/category/allwithevents'

  constructor(private http: HttpClient) { }

  public getCategories(): Observable<any> {
    const headers = {
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Access-Control-Expose-Headers': '*'
    }
    return this.http.get<any>(this.CATEGORY_URL, { headers: headers }).pipe(
      retry(3),
      timeout(15000),
      map(data => data as CategoryDTO[])
    );
  }
}

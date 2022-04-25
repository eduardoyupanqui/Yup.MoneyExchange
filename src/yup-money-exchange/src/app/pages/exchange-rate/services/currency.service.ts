import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Currency } from '../models/currency.model';

@Injectable()
export class CurrencyService {
  urlService: string;
  constructor(private httpClient: HttpClient) {
    this.urlService = environment.urlService;
   }

  getCurrencies(): Observable<Currency[]>
  {
    return this.httpClient.get<Currency[]>(this.urlService + 'currency/all');
  }
}

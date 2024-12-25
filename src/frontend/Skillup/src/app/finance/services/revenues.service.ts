import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { ItemEarnings, Revenue, YearEarnings } from "../models/revenues.model";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class RevenueService {
    private httpClient = inject(HttpClient);

  public getEarningsAndSalesPerCourse(authorId: string) {
    return this.httpClient.get<ItemEarnings[]>(
      environment.apiUrl + '/Finances/Revenues/Author/' + authorId + '/per-courses'
    );
  }

  public getMonthlyEarningsAndSalesPerCourse(authorId: string, year: number){
    return this.httpClient.get<YearEarnings>(
        environment.apiUrl + '/Finances/Revenues/Author/' + authorId + '/monthly?year=' + year);
  }

  public getRevenue(authorId: string){
    return this.httpClient.get<Revenue>(
        environment.apiUrl + '/Finances/Revenues/Author/' + authorId);
  }
}

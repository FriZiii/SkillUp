import { inject, Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";

@Injectable({ providedIn: 'root' })
export class CertificateService {
    private httpClient = inject(HttpClient);

    getCertificate(courseId: string) {
        return this.httpClient.get(environment.apiUrl + '/Courses/' + courseId +'/CompletionCertificate', {
            responseType: 'blob'
          });
    }
}
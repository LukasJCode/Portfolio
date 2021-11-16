import { Injectable } from '@angular/core';
import { ProjectDetails } from './project-details.model';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProjectDetailsService {

  constructor(private http:HttpClient) { }

  formData:ProjectDetails = new ProjectDetails();
  readonly baseURL = 'http://localhost:8080/projects';

  postProjectDetails(){
    return this.http.post(this.baseURL, this.formData);
  }
}

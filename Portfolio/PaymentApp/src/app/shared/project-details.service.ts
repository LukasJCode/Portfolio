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
  list:ProjectDetails[];

  postProjectDetails()
  {
    return this.http.post(this.baseURL, this.formData);
  }

  getAllProjects()
  {
    this.http.get(this.baseURL).toPromise()
    .then(res => this.list = res as ProjectDetails[]);
  }

  updateProject()
  {
    return this.http.put(this.baseURL+"/"+this.formData.id, this.formData);
  }

  deleteProject(id:string)
  {
    return this.http.delete(this.baseURL+"/"+id);
  }
}

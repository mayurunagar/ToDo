import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDo } from '../models/todo';

@Injectable()
export class ToDoService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private url: string) { }

  api = `${this.url}api/todo`;

  getToDos(): Observable<ToDo[]> {
    console.log(this.url, this.api);
    return this.http.get<ToDo[]>(`${this.api}/Get`);
  }

  public addOrUpdateToDo(todo: ToDo) {
    return this.http.post(`${this.api}/AddOrUpdate`, todo);
  }
}

import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToDo } from '../models/todo';
import { ToDoService } from '../services/todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  providers: [ToDoService]
})
export class ToDoComponent {
  public todos: ToDo[];
  public todo: ToDo;

  isAdd = false;
  invalidTitle = false;

  modalReference: any;

  constructor(http: HttpClient, public todoService: ToDoService, private _formBuilder: FormBuilder, private modalService: NgbModal) {
    this.getToDos();
  }

  getToDos() {
    this.todoService.getToDos().subscribe(t => this.todos = t, error => console.log(error));
  }

  saveToDo() {
    if (!this.todo.title || this.todo.title.length == 0) {
      this.invalidTitle = true;
    }
    else {
      this.invalidTitle = false;

      this.todoService.addOrUpdateToDo(this.todo).subscribe((result: any) => {
        this.modalReference.close();
        this.todo = new ToDo();
        this.getToDos();
      }, error => console.log(error));
    }
  }

  addModal(content) {
    this.isAdd = true;
    this.invalidTitle = false;
    this.todo = new ToDo();
    this.modalReference = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  editModal(content, todo: ToDo) {
    this.isAdd = false;
    this.invalidTitle = false;
    this.todo = JSON.parse(JSON.stringify(todo));
    this.modalReference = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
}

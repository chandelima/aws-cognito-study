import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { UnsubscriberService } from '../../core/services/unsubscriber.service';
import { IReadToDo } from '../../interfaces/to-do/read-to-do.interface';
import { ToDoService } from '../services/to-do.service';
import { IUpdateTodo } from '../../interfaces/to-do/update-to-do.interface';
import { ICreateToDo } from '../../interfaces/to-do/create-to-do.interface';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrl: './tasks.component.scss'
})
export class TasksComponent implements AfterViewInit {

  @ViewChild('input') input!: ElementRef<HTMLInputElement>;

  private _toDoList: IReadToDo[] = [];
  private _activeTodo: IReadToDo | null = null;

  inputValue: string | null = null;

  get toDoList(): IReadToDo[] {
    return this._toDoList;
  }

  get activeTodo(): IReadToDo | null {
    return this._activeTodo;
  }

  constructor(
    private readonly _unsubscriberService: UnsubscriberService,
    private readonly _toDoService: ToDoService
  ) {
    this.getAllTasks();
  }

  ngAfterViewInit(): void {
    this.setInputFocus();
  }

  private getAllTasks(): void {
    this._toDoService
      .getAll()
      .pipe(this._unsubscriberService.takeUntilDestroy)
      .subscribe(response => {
        if (Array.isArray(response?.data)) {
          this._toDoList = response.data;
        }
      })
  }

  private setInputFocus(): void {
    this.input.nativeElement.focus();
  }

  private clearInput(): void {
    this._activeTodo = null;
    this.inputValue = null;
    this.setInputFocus();
  }

  public setActive(toDo: IReadToDo): void {
    this._activeTodo = toDo;
    this.inputValue = toDo.description;
  }

  public isCompleted(task: IReadToDo): boolean {
    return !!task?.completionDate;
  }

  public inputHasContent(): boolean {
    return !!this.inputValue?.length;
  }

  public onInputValueChange(value: string) {
    if (!value.length) {
      this._activeTodo = null;
    }
  }

  public create(data: ICreateToDo) {
    this._toDoService
      .create(data)
      .pipe(this._unsubscriberService.takeUntilDestroy)
      .subscribe(() => {
        this.clearInput();
        this.getAllTasks();
      })
  }

  public update(data: IUpdateTodo) {
    this._toDoService
      .update(data)
      .pipe(this._unsubscriberService.takeUntilDestroy)
      .subscribe(() => {
        this.clearInput();
        this.getAllTasks();
      })
  }

  public save() {
    if (this._activeTodo) {
      const payload: IUpdateTodo = { ...this._activeTodo };
      payload.description = this.inputValue ?? '';

      this.update(payload);
    }
    else
    {
      const payload: ICreateToDo = {
        description: this.inputValue ?? undefined
      };

      this.create(payload);
    }
  }

  public delete(id: string) {
    this._toDoService
      .delete(id)
      .pipe(this._unsubscriberService.takeUntilDestroy)
      .subscribe(() => {
        this.clearInput();
        this.getAllTasks();
      });
  }

  public setStatus(data: IReadToDo) {
    const payload = { ...data };

    if (payload?.completionDate) {
      payload.completionDate = undefined;
    } else {
      payload.completionDate = new Date();
    }

    this.update(payload);
  }
}

import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  private _loadingSubject = new BehaviorSubject<boolean>(false);
  private _counter = 0;

  constructor(
    private readonly ngxSpinner: NgxSpinnerService
  ) { }

  get loading$() {
    return this._loadingSubject.asObservable();
  }

  show() {
    this._counter++;
    this.updateSpinnerState();
  }

  hide() {
    this._counter = Math.max(0, this._counter - 1);
    this.updateSpinnerState();
  }

  private updateSpinnerState() {
    const isLoading = this._counter > 0;
    if (isLoading === this._loadingSubject.value) {
      return;
    }

    this._loadingSubject.next(isLoading);

    if (isLoading) {
      this.ngxSpinner.show();
    } else {
      this.ngxSpinner.hide();
    }
  }
}

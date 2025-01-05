import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { TasksComponent } from './features/tasks/tasks.component';

@NgModule({
  declarations: [
    AppComponent,
    TasksComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    BrowserModule,
    CoreModule,
    FormsModule
  ],
  providers: [
    provideHttpClient(
      withInterceptors([ ...CoreModule.interceptors ])
    ),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

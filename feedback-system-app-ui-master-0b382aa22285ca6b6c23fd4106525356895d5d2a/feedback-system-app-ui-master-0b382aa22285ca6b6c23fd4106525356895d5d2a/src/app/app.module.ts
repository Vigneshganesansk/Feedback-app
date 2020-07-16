import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatSnackBarModule, MatExpansionModule, MatToolbarModule, MatCardModule, MatIconModule, MatSidenavModule, MatListModule, MatButtonModule, MatGridListModule, MatMenuModule, MatRippleModule, MAT_RIPPLE_GLOBAL_OPTIONS, MatRadioModule, MatCheckboxModule } from  '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { DxDataGridModule, DxTemplateModule, DxButtonModule } from 'devextreme-angular';

import { NgxDropzoneModule } from 'ngx-dropzone';



import { appRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { FooterComponent } from './footer/footer.component';
import { EventsComponent } from './events/events.component';
import { ReportsComponent } from './reports/reports.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { UploadComponent } from './upload/upload.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ToolbarComponent,
    FooterComponent,
    EventsComponent,
    ReportsComponent,
    ConfigurationComponent,
    FeedbackComponent,
    UploadComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    appRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    DxDataGridModule,
    DxTemplateModule,
    DxButtonModule,

    NgxDropzoneModule,

    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatExpansionModule,
    MatGridListModule,
    MatMenuModule,
    MatRippleModule,
    MatRadioModule,
    MatCheckboxModule,

    BrowserAnimationsModule,
    FlexLayoutModule,
    MatSnackBarModule,
  ],
  providers: [
    // {provide: MAT_RIPPLE_GLOBAL_OPTIONS, useValue: {disabled: true}} // removes animation effect
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

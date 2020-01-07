import { Controller, Get, Render, Post, Body } from '@nestjs/common';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  @Render('index')
  getHello() {
    return { message: 'Hello world!' };
  }
  @Post()
  @Render('index')
  postHello(@Body() data) {
    return { message: data.firstName };
  }
}

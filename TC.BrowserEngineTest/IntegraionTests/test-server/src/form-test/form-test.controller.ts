import { Controller, Get, Render, Post, Body } from '@nestjs/common';

@Controller('form-test')
export class FormTestController {
    @Get()
    @Render('form-test')
    getHello() {
        return { message: 'Hello world!' };
    }
    @Post()
    @Render('form-test')
    postHello(@Body() data) {
        return { message: data.firstName };
    }
}

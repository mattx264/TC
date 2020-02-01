import { Controller, Get, Render, Res } from '@nestjs/common';

@Controller('ajax-test')
export class AjaxTestController {
    
    @Get()
    @Render('ajax-test/index')
    Index() {
        return { message: 'Hello world!' };
    }
    @Get('get1')
    async get1(@Res() res) {
        setTimeout((function() {res.send("")}), 2000);

    }
    @Get('get2')
    async get2(@Res() res) {
        res.send("OK");
    }
    
}

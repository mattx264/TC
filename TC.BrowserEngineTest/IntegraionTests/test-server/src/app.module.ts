import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { FormTestController } from './form-test/form-test.controller';
import { AjaxTestController } from './ajax-test/ajax-test.controller';

@Module({
  imports: [],
  controllers: [AppController, FormTestController, AjaxTestController],
  providers: [AppService],
})
export class AppModule {}

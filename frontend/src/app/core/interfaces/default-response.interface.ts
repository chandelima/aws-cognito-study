import { IMessage } from "./message.interface";

export interface IDefaultResponse<T = any> {
  data: T;
  messages: IMessage[];
}

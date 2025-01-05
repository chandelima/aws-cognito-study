export interface IMessage {
  type: IMessageType;
  message: string;
}

export type IMessageType = 'Error' | 'Information' | 'Success';

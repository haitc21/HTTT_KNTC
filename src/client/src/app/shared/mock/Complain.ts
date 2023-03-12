export class Complain {
    code: string;
    title: string;
    sender: string;
    area: string;
    sentDate: Date | "";
    typeHoSo: typesHoSo;
    fieldType: fieldsHoSo;
    returnDate1: Date | "";
    result1: boolean;
    returnDate2: Date | "";
    result2: boolean;
    latLng: [number, number];
    
}
export enum typesHoSo {
    Complaint,
    Accusation
}
export enum fieldsHoSo {
    Land,
    Emviroment,
    Water,
    Mineral
}

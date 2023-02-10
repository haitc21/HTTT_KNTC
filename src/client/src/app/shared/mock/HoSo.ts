export class HoSo {
    code: string;
    title: string;
    sender: string;
    area: string;
    sentDate: string;
    typeHoSo: typesHoSo;
    fieldType: fieldsHoSo;
    returnDate1: string;
    result1: string;
    returnDate2: string;
    result2: string;
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

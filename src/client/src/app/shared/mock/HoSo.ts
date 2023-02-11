export class HoSo {
    code: string;
    title: string;
    sender: string;
    area: string;
    sentDate: Date | "";
    typeHoSo: typesHoSo;
    fieldType: fieldsHoSo;
    returnDate1: Date | "";
    result1: string;
    returnDate2: Date | "";
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

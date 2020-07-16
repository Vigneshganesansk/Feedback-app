export class UserModel{
    associateId: number;
    associateName: string;
    email: string;
    roleId?: number;
    designation: string;
    buid?: number;
    addedUserId?: number;
    addedDate?: Date;
    isDeleted: boolean;
    deletedUserId?: number;
    deletedDate: string;
}
export class Announcement {
  id: number;
  title: string | null;
  description: string | null;
  dateAdded: Date;

  constructor(id: number, title: string | null = null, description: string | null = null) {
    this.id = id;
    this.title = title;
    this.description = description;
    this.dateAdded = new Date();
  }
}

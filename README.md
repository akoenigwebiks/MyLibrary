### Finalized Library Management System Specification for MVP

[Current Diagram](https://dbdiagram.io/d/Library-66ab8aeb8b4bb5230ef7b9ac)


**Version 1.3**

**Objective:** Develop a streamlined MVP for an ASP.NET MVC application using Entity Framework Core to manage multiple types of libraries (e.g., math, music), each housing shelves, books, and book sets. The MVP focuses exclusively on creation and browsing functionalities.

---

### System Overview

The MVP of the Library Management System (LMS) will enable:

1. **Create Multiple Libraries**: Allow the addition of various libraries by type, such as math, music, etc.
2. **Create Shelves in Libraries**: Add shelves with specific dimensions to existing libraries.
3. **Create Books**: Add books, optionally linking them to shelves and book sets.
4. **Create Book Sets**: Group books into thematic or series-based sets.
5. **Browse Entities**: Enable viewing of libraries, shelves, books, and book sets through the system.

---

### Entities and Relationships

#### Library
- **Attributes**:
  - LibraryId (int, PK)
  - Name (string)
  - Type (string) - e.g., Math, Music
  - Location (string)
- **Relationships**:
  - Has many Shelves
  - Has many Books indirectly through Shelves

#### Shelf
- **Attributes**:
  - ShelfId (int, PK)
  - LibraryId (int, FK)
  - Number (string)
  - Height (decimal)
  - Width (decimal)
- **Relationships**:
  - Belongs to Library
  - Has many Books

#### Book
- **Attributes**:
  - BookId (int, PK)
  - ShelfId (int, FK, nullable)
  - Title (string)
  - Genre (string)
  - Height (decimal)
  - Thickness (decimal)
- **Relationships**:
  - Optional: Belongs to Shelf
  - Optional: Belongs to BookSet

#### Book Set
- **Attributes**:
  - BookSetId (int, PK)
  - Title (string)
  - Description (string, nullable)
- **Relationships**:
  - Has many Books

---

### Business Logic

1. **Shelf Compatibility**: `CanAccommodateBook(Book book)` method in the `Shelf` entity ensures that book dimensions fit within the shelf's dimensions.
2. **Book Set Compatibility**: `CanAccommodateBookSet(BookSet bookSet)` method in the `Shelf` entity ensures that the entire set can be accommodated based on total thickness.

---

### Data Handling and Restrictions

- **SQL Level Cascade**: Cascade delete rules are implemented at the database level, facilitating future feature expansions.
- **No Delete Operations**: Users are currently not allowed to delete any entities.
- **No Update Operations**: Entities cannot be updated in the current version. All entries are final upon creation.

---

### Security and Performance

- Plan for future implementation of authentication and authorization mechanisms.
- Consider future performance optimization, including indexing on frequently queried fields like LibraryId and ShelfId.

---

This specification ensures the system can manage multiple library types and maintains a clear relationship between libraries and the books they house, albeit indirectly through shelves. This approach sets a focused direction for the MVP, emphasizing essential functionalities while allowing room for future enhancements.

## GPT Conversation used to facilitate this design
[Link](https://chatgpt.com/share/ebbf0f9f-6911-4736-9911-74cc90d0610b)
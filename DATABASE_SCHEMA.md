# PostgreSQL Database Schema

## Entity Definitions

### users

Stores visitors, receptionists, and admins who interact with the AI receptionist system.

| Column | Type | Notes |
| --- | --- | --- |
| id | uuid | Primary key |
| full_name | varchar(150) | Required |
| email | varchar(256) | Optional, unique when present |
| phone_number | varchar(30) | Optional |
| role | varchar(30) | Visitor, Receptionist, Admin |
| is_active | boolean | Defaults to true |
| created_at_utc | timestamptz | Required |
| updated_at_utc | timestamptz | Optional |

### chat_sessions

Represents one conversation between a visitor/user and the chatbot.

| Column | Type | Notes |
| --- | --- | --- |
| id | uuid | Primary key |
| user_id | uuid | Optional foreign key to users.id |
| status | varchar(30) | Active, Escalated, Closed |
| channel | varchar(50) | Web, SMS, WhatsApp, etc. |
| visitor_ip_address | varchar(45) | Supports IPv4 and IPv6 |
| summary | varchar(2000) | Optional conversation summary |
| ended_at_utc | timestamptz | Optional |
| created_at_utc | timestamptz | Required |
| updated_at_utc | timestamptz | Optional |

### messages

Stores every user, assistant, system, or staff message in a chat session.

| Column | Type | Notes |
| --- | --- | --- |
| id | uuid | Primary key |
| chat_session_id | uuid | Required foreign key to chat_sessions.id |
| role | varchar(30) | User, Assistant, System, Staff |
| content | varchar(8000) | Required |
| intent_name | varchar(100) | Optional detected intent |
| confidence_score | numeric(5,4) | Optional AI confidence score |
| response_time | interval | Optional response duration |
| is_helpful | boolean | Defaults to false |
| created_at_utc | timestamptz | Required |
| updated_at_utc | timestamptz | Optional |

### faqs

Stores searchable answers used by the receptionist chatbot.

| Column | Type | Notes |
| --- | --- | --- |
| id | uuid | Primary key |
| question | varchar(500) | Required |
| answer | varchar(4000) | Required |
| category | varchar(100) | Required |
| keywords | varchar(500) | Optional |
| display_order | integer | Defaults to 0 |
| is_active | boolean | Defaults to true |
| created_at_utc | timestamptz | Required |
| updated_at_utc | timestamptz | Optional |

### company_information

Stores receptionist-facing company details such as contact data and business hours.

| Column | Type | Notes |
| --- | --- | --- |
| id | uuid | Primary key |
| company_name | varchar(200) | Required |
| description | varchar(2000) | Optional |
| address | varchar(500) | Optional |
| phone_number | varchar(30) | Optional |
| email | varchar(256) | Optional |
| website_url | varchar(500) | Optional |
| business_hours | varchar(1000) | Optional |
| is_active | boolean | Defaults to true |
| created_at_utc | timestamptz | Required |
| updated_at_utc | timestamptz | Optional |

## Relationships

```text
users 1 -> many chat_sessions
chat_sessions 1 -> many messages
faqs has no required relationship
company_information has no required relationship
```

## Primary Keys

All tables use `id` as a UUID primary key.

## Foreign Keys

| Table | Column | References | Delete Behavior |
| --- | --- | --- | --- |
| chat_sessions | user_id | users.id | Set null |
| messages | chat_session_id | chat_sessions.id | Cascade |

## Index Recommendations

| Table | Index | Purpose |
| --- | --- | --- |
| users | unique email where email is not null | Fast login/profile lookup and duplicate prevention |
| users | phone_number | Phone-based visitor lookup |
| users | role | Admin/receptionist filtering |
| users | created_at_utc | Reporting and recent-user lists |
| chat_sessions | user_id | Load a user's sessions |
| chat_sessions | status | Find active/escalated chats quickly |
| chat_sessions | created_at_utc | Recent session dashboards |
| chat_sessions | user_id, created_at_utc | User conversation history ordered by time |
| messages | chat_session_id | Load messages for a session |
| messages | created_at_utc | Message timeline/reporting |
| messages | intent_name | Intent analytics |
| messages | chat_session_id, created_at_utc | Ordered chat transcript retrieval |
| faqs | category | Browse FAQs by category |
| faqs | is_active | Filter published chatbot answers |
| faqs | is_active, category, display_order | FAQ display and chatbot retrieval |
| company_information | is_active | Fetch current active company profile |
| company_information | company_name | Admin search |

## EF Core Files

Entity classes are in `src/ReceptionistChatBot.Domain/Entities`.

Fluent API configuration classes are in `src/ReceptionistChatBot.Infrastructure/Persistence/Configurations`.

The database context is `ReceptionistChatBotDbContext` in `src/ReceptionistChatBot.Infrastructure/Persistence`.

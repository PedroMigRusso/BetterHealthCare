# 🏥 Better Health Care – REST API

## 📌 Project Summary

**Better Health Care** is a RESTful API designed to centralize and standardize access to patient medical information across multiple healthcare institutions.

The project focuses on **backend architecture, API design, and data modeling**, simulating a real-world healthcare integration scenario where patient data must be shared securely and consistently between public and private entities.

This repository contains **only the backend API**, built with scalability and future integrations in mind.

---

## 🧠 Problem Context

In healthcare systems, patient medical data is often fragmented across multiple institutions.  
This lack of interoperability leads to:

- Incomplete patient histories  
- Duplicate medical exams  
- Slower and less accurate diagnoses  

These issues become especially visible when patients move between **public and private healthcare providers**.

---

## 💡 Solution Overview

The Better Health Care API provides:

- A **centralized access point** for patient medical data  
- A **REST-based contract** that can be consumed by multiple systems  
- Clear separation between patients, procedures, actions, and medical files  

The API does not enforce any specific UI or client implementation, allowing each institution to consume the data according to its own needs.

---

## 🧱 Core Domain Model

- **Patient** – Represents a registered patient  
- **PatientAction** – A medical action performed on a patient (exam, surgery, consultation)  
- **Procedure** – A reusable definition of a medical procedure  
- **MedicalFile** – Medical documents associated with patient actions  

Relationships are designed to preserve historical integrity while remaining flexible for future extensions.

---

## 🔌 API Capabilities

### Patient Management
- Create, retrieve, update, and delete patients  
- Retrieve complete patient history with associated medical actions  

### Medical Actions
- Register medical actions performed on a patient  
- Associate procedures and medical files  
- Retrieve historical actions independently  

### Procedure Catalog
- Centralized list of medical procedures  
- Reusable across patient actions  
- Designed to avoid breaking historical records  

### Medical Files
- Upload and manage medical documents  
- Files are referenced by patient actions to maintain loose coupling  

---

## 📑 Endpoints Overview

### 🧍 Patients

| Method | Endpoint | Description |
|------|--------|------------|
| GET | `/patients` | Retrieve all patients |
| GET | `/patients/{id}` | Retrieve patient by ID |
| GET | `/patients/{id}/full` | Retrieve patient with full medical history |
| POST | `/patients` | Create a new patient |
| PUT | `/patients/{id}` | Update patient |
| DELETE | `/patients/{id}` | Delete patient |

---

### 📋 Patient Actions

| Method | Endpoint | Description |
|------|--------|------------|
| GET | `/patients/{patientId}/actions` | Retrieve all actions for a patient |
| GET | `/actions/{actionId}` | Retrieve action by ID |
| POST | `/patients/{patientId}/actions` | Create a new patient action |
| PUT | `/patients/{patientId}/actions/{actionId}` | Update patient action |
| DELETE | `/patients/{patientId}/actions/{actionId}` | Delete patient action |

---

### 🧪 Procedures

| Method | Endpoint | Description |
|------|--------|------------|
| GET | `/procedures` | Retrieve all procedures |
| GET | `/procedures/{id}` | Retrieve procedure by ID |
| POST | `/procedures` | Create a new procedure |
| PUT | `/procedures/{id}` | Update procedure *(restricted)* |
| DELETE | `/procedures/{id}` | Delete procedure *(restricted)* |

> ⚠️ Update and delete operations should be restricted in production environments to avoid breaking historical medical data.

---

### 📁 Medical Files

| Method | Endpoint | Description |
|------|--------|------------|
| GET | `/files` | Retrieve all medical files |
| GET | `/files/{id}` | Retrieve medical file by ID |
| POST | `/files` | Create a new medical file |
| PUT | `/files/{id}` | Update medical file |
| DELETE | `/files/{id}` | Delete medical file |

---

## 🧩 Example Use Cases
- Register a patient and track their medical history over time
  
- Share medical records between healthcare providers

- Retrieve medical files from procedures performed in different institutions

- Maintain historical consistency of medical actions and procedures

## 🛠️ Technical Stack
- ASP.NET Core

- RESTful API design

- Entity Framework Core

- SQL Database

- DTO-based architecture


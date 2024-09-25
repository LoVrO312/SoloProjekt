import React, { useState, useEffect } from 'react';
import SubmitForm from './SubmitForm';
import SubjectTable from './SubjectTable';
import Modal from './Modal';

function SubjectCrud() {
  const [subject, setSubject] = useState({
    id: '',
    name: '',
    ects: '',
    departmentId: '',
    timeCreated: ''
  });

  const [subjects, setSubjects] = useState(() => {
    const storedSubjects = localStorage.getItem('subjects');
    return storedSubjects ? JSON.parse(storedSubjects) : [];
  });
  
  const [showModal, setShowModal] = useState(false);
  const [subjectToUpdate, setSubjectToUpdate] = useState(null);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setSubject({ ...subject, [name]: value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setSubjects([...subjects, subject]);
  };

  const handleUpdate = (subject) => {
    setSubjectToUpdate(subject); 
    setShowModal(true);          
  };

  const handleUpdateSubmit = (e) => {
    e.preventDefault();
    setSubjects(subjects.map(s => (s.id === subjectToUpdate.id ? subjectToUpdate : s)));
    setShowModal(false);
  };

  const handleModalInputChange = (e) => {
    const { name, value } = e.target;
    setSubjectToUpdate({ ...subjectToUpdate, [name]: value });
  };

  const handleDelete = (id) => {
    const updatedSubjects = subjects.filter(subject => subject.id !== id);
    setSubjects(updatedSubjects); 
  };

  return (
    <>
      <div className="submit-form">
        <h1>Title</h1>
        <SubmitForm
          subject={subject}
          handleInputChange={handleInputChange}
          handleSubmit={handleSubmit}
        />
      </div>
      <div>
        <h1>Table of subjects</h1>
        <SubjectTable 
          subjects={subjects}
          handleUpdate={handleUpdate}
          handleDelete={handleDelete}
        />
      </div>
      <Modal
        show={showModal}
        onClose={() => setShowModal(false)}
        subject={subjectToUpdate}
        handleInputChange={handleModalInputChange}
        handleUpdateSubmit={handleUpdateSubmit}
      />
    </>
  );
}

export default SubjectCrud;

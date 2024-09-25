import React from 'react';

function Modal({ show, onClose, subject, handleInputChange, handleUpdateSubmit }) {
  if (!show) {
    return null;
  }

  return (
    <div className="modal-backdrop">
      <div className="modal-content">
        <h2>Update Subject</h2>
        <form onSubmit={handleUpdateSubmit}>
          <input
            type="text"
            name="name"
            value={subject.name}
            onChange={handleInputChange}
            placeholder="Name"
            required
          />
          <br />
          <input
            type="number"
            name="ects"
            value={subject.ects}
            onChange={handleInputChange}
            placeholder="ECTS"
            required
          />
          <br />
          <input
            type="number"
            name="departmentId"
            value={subject.departmentId}
            onChange={handleInputChange}
            placeholder="Department ID"
            required
          />
          <br />
          <input
            type="datetime-local"
            name="timeCreated"
            value={subject.timeCreated}
            onChange={handleInputChange}
            placeholder="Time Created"
            required
          />
          <br />
          <button type="submit">Update</button>
          <button type="button" onClick={onClose}>Cancel</button>
        </form>
      </div>
    </div>
  );
}

export default Modal;

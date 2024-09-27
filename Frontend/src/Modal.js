import React from 'react';

function Modal({ show, onClose, subject, handleInputChange, handleUpdateSubmit }) {
  if (!show) {
    return null;
  }

  return (
    <div className="modal-backdrop">
      <div className="modal-content">
        <h2>Update Subject's department</h2>
        <form onSubmit={handleUpdateSubmit}>
          <br />
          <input
            type="text"
            name="departmentId"
            value={subject.departmentId}
            onChange={handleInputChange}
            placeholder="new department ID"
            required
          />
          <button type="submit">Update</button>
          <button type="button" onClick={onClose}>Cancel</button>
        </form>
      </div>
    </div>
  );
}

export default Modal;

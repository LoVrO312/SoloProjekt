import React from 'react';

function SubmitForm({ subject, handleInputChange, handleSubmit }){
    return (
    <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="id"
          placeholder="Id"
          value={subject.id}
          onChange={handleInputChange}
          required
        />
        <br/>
        <input
          type="text"
          name="name"
          placeholder="Name"
          value={subject.name}
          onChange={handleInputChange}
          required
        />
        <br/>
        <input
          type="number"
          name="ects"
          placeholder="ects points"
          value={subject.ects}
          onChange={handleInputChange}
          required
        />
        <br/>
        <input
          type="number"
          name="departmentId"
          placeholder="department id"
          value={subject.departmentId}
          onChange={handleInputChange}
          required          
        />
        <br/>
        <input
          type="datetime-local"
          name="timeCreated"
          placeholder="time created"
          value={subject.timeCreated}
          onChange={handleInputChange}
          required
        />
        <br/>
        <button type="submit">Insert</button>
    </form>
    );
}

export default SubmitForm;
package com.spartano.tiendamovil.model;

import java.io.Serializable;
import java.util.Date;

public class Usuario implements Serializable {
    public int id;
    public String nombre;
    public String apellido;
    public String dni;
    public String email;
    public String telefono;
    public String clave;
    public int permisos;
    public int estado;
    public String creacion;
    public String permisosNombre;

    public Usuario(){}

    public Usuario(int id, String nombre, String apellido, String dni, String email, String telefono, String clave, int permisos, int estado, String creacion, String permisosNombre) {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.dni = dni;
        this.email = email;
        this.telefono = telefono;
        this.clave = clave;
        this.permisos = permisos;
        this.estado = estado;
        this.creacion = creacion;
        this.permisosNombre = permisosNombre;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getApellido() {
        return apellido;
    }

    public void setApellido(String apellido) {
        this.apellido = apellido;
    }

    public String getDni() {
        return dni;
    }

    public void setDni(String dni) {
        this.dni = dni;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }

    public String getClave() {
        return clave;
    }

    public void setClave(String clave) {
        this.clave = clave;
    }

    public int getPermisos() {
        return permisos;
    }

    public void setPermisos(int permisos) {
        this.permisos = permisos;
    }

    public int getEstado() {
        return estado;
    }

    public void setEstado(int estado) {
        this.estado = estado;
    }

    public String getCreacion() {
        return creacion;
    }

    public void setCreacion(String creacion) {
        this.creacion = creacion;
    }

    public String getPermisosNombre() {
        return permisosNombre;
    }

    public void setPermisosNombre(String permisosNombre) {
        this.permisosNombre = permisosNombre;
    }
}

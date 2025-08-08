// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    const toggle = document.getElementById('togglePassword');
    const pwd = document.getElementById('floatingPassword');
    if (toggle && pwd) {
        toggle.addEventListener('click', function () {
            const isText = pwd.type === 'text';
            pwd.type = isText ? 'password' : 'text';
            toggle.setAttribute('aria-pressed', (!isText).toString());
            toggle.innerHTML = isText
                ? '<i class="bi bi-eye-slash-fill"></i>'
                : '<i class="bi bi-eye-fill"></i>';
        });
    }
});
searchVisible = 0;
transparent = true;

$(document).ready(function () {

    $('[rel="tooltip"]').tooltip();

    var $validator = $('.wizard-card form').validate({
        rules: {
            txtNombre: {
                required: true,
                maxlength: 20
            },
            txtApellidoPaterno: {
                required: true,
                maxlength: 20
            },
            txtApellidoMaterno: {
                required: true,
                maxlength: 20
            },

            txtCelular: {
                required: true,
                minlength: 8
            },
            txtCi: {
                required: true,
                minlength: 7
            },
            txtDireccion: {
                required: true,
                minlength: 4
            },
            txtCorreo: {
                required: true,
                email: true
            },
            txtMunicipio: {
                required: true,
                minlength: 3
            },
            txtUsuario: {
                required: true,
                minlength: 3,
                noSpecialChars: true
            },
            txtContrasena: {
                required: true,
                minlength: 8,
                validatePassword: true
            },
            txtEspecialidad: {
                required: true,
                minlength: 4
            },
            txtTitulacion: {
                required: true,
                date: true,
                validateFechaTitulacion: true
            },
            txtFechaNacimiento: {
                required: true,
                date: true,
                validateFechaNacimiento: true

            },
            fileUpload_type: {
                required: true,
                validateFileType: true
            }, fileUpload_type2: {
                required: true,
                validateFileType: true
            }, fileUpload_type3: {
                required: true,
                validateFileType: true,
                validateFileType_image: true
            }
        },
        messages: {
            txtCorreo: {
                required: "Por favor, introduce tu correo electrónico.",
                email: "Por favor, introduce un correo electrónico válido."
            },
            txtUsuario: {
                required: "Por favor, introduce tu nombre de usuario.",
                noSpecialChars: "El usuario no debe contener la letra 'ñ'."
            },
            txtFechaNacimiento: {
                required: "Por favor, introduce tu fecha de nacimiento.",
                date: "Por favor, introduce una fecha válida."
            },
            fileUpload_type: {
                required: "Por favor, selecciona una archivo.",
                validateFileType: "Por favor, selecciona un archivo válido."
            },
            txtTitulacion: {
                required: "Por favor, introduce tu fecha de titulación.",
                date: "Por favor, introduce una fecha de titulación válida."
            },
            txtContrasena: {
                required: "Por favor, introduce tu contraseña.",
                minlength: "La contraseña debe tener al menos 8 caracteres.",
                validatePassword: "La contraseña debe contener letras minúsculas y mayúsculas, al menos un carácter especial."
            },
        },
    });
    $.validator.addMethod("noSpecialChars", function (value, element) {
        var specialCharRegex = /ñ/i;

        return !specialCharRegex.test(value);
    }, "Este campo no debe contener la letra 'ñ'.");

    $.validator.addMethod("validatePassword", function (value, element) {
        var lowercaseRegex = /^(?=.*[a-z])/;
        var uppercaseRegex = /^(?=.*[A-Z])/;
        var specialCharRegex = /^(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-])/;

        return lowercaseRegex.test(value) && uppercaseRegex.test(value) && specialCharRegex.test(value);
    }, "La contraseña debe contener letras minúsculas y mayúsculas, al menos un carácter especial.");

    $.validator.addMethod("validateFileType", function (value, element) {
        return element.files.length > 0;
    }, "Por favor, selecciona un archivo.");

    $.validator.addMethod("validateFileType_image", function (value, element) {
        var fileExtension = value.split('.').pop().toLowerCase();

        var allowedExtensions = ['png', 'jpg', 'jpeg'];
        return allowedExtensions.indexOf(fileExtension) !== -1;
    }, "Por favor, selecciona una imagen con formato válido (png, jpg o jpeg).");

    $.validator.addMethod("validateFechaNacimiento", function (value, element) {
        var today = new Date();
        var birthDate = new Date(value);
        var minDate = new Date(today.getFullYear() - 70, today.getMonth(), today.getDate());
        var maxDate = new Date(today.getFullYear() - 18, today.getMonth(), today.getDate());
        return birthDate >= minDate && birthDate <= maxDate;
    }, "Por favor, introduce una fecha válida.");

    $.validator.addMethod("validateFechaTitulacion", function (value, element) {
        var fechaTitulacion = new Date(value);
        var fechaLimiteSuperiorTitulacion = new Date();
        var fechaLimiteInferiorTitulacion = new Date();
        fechaLimiteInferiorTitulacion.setFullYear(fechaLimiteInferiorTitulacion.getFullYear() - 85);

        return fechaTitulacion <= fechaLimiteSuperiorTitulacion && fechaTitulacion >= fechaLimiteInferiorTitulacion;
    }, "Por favor, introduce una fecha válida.");


    // Wizard Initialization
    $('.wizard-card').bootstrapWizard({
        'tabClass': 'nav nav-pills',
        'nextSelector': '.btn-next',
        'previousSelector': '.btn-previous',

        onNext: function (tab, navigation, index) {
            var $valid = $('.wizard-card form').valid();
            if (!$valid) {
                $validator.focusInvalid();
                return false;
            }
        },

        onInit: function (tab, navigation, index) {

            //check number of tabs and fill the entire row
            var $total = navigation.find('li').length;
            $width = 100 / $total;
            var $wizard = navigation.closest('.wizard-card');

            $display_width = $(document).width();

            if ($display_width < 600 && $total > 3) {
                $width = 50;
            }

            navigation.find('li').css('width', $width + '%');
            $first_li = navigation.find('li:first-child a').html();
            $moving_div = $('<div class="moving-tab">' + $first_li + '</div>');
            $('.wizard-card .wizard-navigation').append($moving_div);
            refreshAnimation($wizard, index);
            $('.moving-tab').css('transition', 'transform 0s');
        },

        onTabClick: function (tab, navigation, index) {

            var $valid = $('.wizard-card form').valid();

            if (!$valid) {
                return false;
            } else {
                return true;
            }
        },

        onTabShow: function (tab, navigation, index) {
            var $total = navigation.find('li').length;
            var $current = index + 1;

            var $wizard = navigation.closest('.wizard-card');

            // If it's the last tab then hide the last button and show the finish instead
            if ($current >= $total) {
                $($wizard).find('.btn-next').hide();
                $($wizard).find('.btn-finish').show();
            } else {
                $($wizard).find('.btn-next').show();
                $($wizard).find('.btn-finish').hide();
            }

            button_text = navigation.find('li:nth-child(' + $current + ') a').html();

            setTimeout(function () {
                $('.moving-tab').text(button_text);
            }, 150);

            var checkbox = $('.footer-checkbox');

            if (!index == 0) {
                $(checkbox).css({
                    'opacity': '0',
                    'visibility': 'hidden',
                    'position': 'absolute'
                });
            } else {
                $(checkbox).css({
                    'opacity': '1',
                    'visibility': 'visible'
                });
            }

            refreshAnimation($wizard, index);
        }
    });


    // Prepare the preview for profile picture
    $("#wizard-picture").change(function () {
        readURL(this);
    });

    $('[data-toggle="wizard-radio"]').click(function () {
        wizard = $(this).closest('.wizard-card');
        wizard.find('[data-toggle="wizard-radio"]').removeClass('active');
        $(this).addClass('active');
        $(wizard).find('[type="radio"]').removeAttr('checked');
        $(this).find('[type="radio"]').attr('checked', 'true');
    });

    $('[data-toggle="wizard-checkbox"]').click(function () {
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $(this).find('[type="checkbox"]').removeAttr('checked');
        } else {
            $(this).addClass('active');
            $(this).find('[type="checkbox"]').attr('checked', 'true');
        }
    });

    $('.set-full-height').css('height', 'auto');

});



//Function to show image before upload

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#wizardPicturePreview').attr('src', e.target.result).fadeIn('slow');
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(window).resize(function () {
    $('.wizard-card').each(function () {
        $wizard = $(this);
        index = $wizard.bootstrapWizard('currentIndex');
        refreshAnimation($wizard, index);

        $('.moving-tab').css({
            'transition': 'transform 0s'
        });
    });
});

function refreshAnimation($wizard, index) {
    total_steps = $wizard.find('li').length;
    move_distance = $wizard.width() / total_steps;
    step_width = move_distance;
    move_distance *= index;

    $wizard.find('.moving-tab').css('width', step_width);
    $('.moving-tab').css({
        'transform': 'translate3d(' + move_distance + 'px, 0, 0)',
        'transition': 'all 0.3s ease-out'

    });
}



const selectImage = document.querySelector('.select-image');
const inputFile = document.querySelector('#fileUpload');
const imgArea = document.querySelector('.img-area');

selectImage.addEventListener('click', function () {
    inputFile.click();
})

inputFile.addEventListener('change', function () {
    const image = this.files[0]
    if (image.size < 2000000) {
        const reader = new FileReader();
        reader.onload = () => {
            const allImg = imgArea.querySelectorAll('img');
            allImg.forEach(item => item.remove());
            const imgUrl = reader.result;
            const img = document.createElement('img');
            img.src = imgUrl;
            imgArea.appendChild(img);
            imgArea.classList.add('active');
            imgArea.dataset.img = image.name;
        }
        reader.readAsDataURL(image);
    } else {
        alert("La imagen no puede pesar mas de 2MB");
    }
})




function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
};

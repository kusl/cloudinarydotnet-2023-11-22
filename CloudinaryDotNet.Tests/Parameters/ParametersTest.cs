﻿using System;
using System.Collections.Generic;
using System.IO;
using CloudinaryDotNet.Actions;
using NUnit.Framework;

namespace CloudinaryDotNet.Tests.Parameters
{
    [TestFixture]
    public class ParametersTest
    {
        [Test]
        public void TestArchiveParamsCheck()
        {
            Assert.Throws<ArgumentException>(new ArchiveParams().Check, "Should require atleast on option specified: PublicIds, Tags or Prefixes");
        }

        [Test]
        public void TestCreateTransformParamsCheck()
        {
            var p = new CreateTransformParams();
            Assert.Throws<ArgumentException>(p.Check, "Should require Name");
            p.Name = "some_name";
            Assert.Throws<ArgumentException>(p.Check, "Should require Transformation");
        }

        [Test]
        public void TestDelDerivedResParamsCheck()
        {
            var p = new DelDerivedResParams();
            Assert.Throws<ArgumentException>(p.Check, "Should require either DerivedResources or Tranformations not null");

            p.DerivedResources = new List<string>();
            Assert.Throws<ArgumentException>(p.Check, "Should require at least on item in either DerivedResources or Tranformations specified");

            p.Transformations = new List<Transformation>() { new Transformation() };
            Assert.Throws<ArgumentException>(p.Check, "Should require PublicId");
        }

        [Test]
        public void TestDeletionParamsCheck()
        {
            Assert.Throws<ArgumentException>(new DeletionParams("").Check, "Should require PublicId");
        }

        [Test]
        public void TestDelResParamsCheck()
        {
            var p = new DelResParams();
            Assert.Throws<ArgumentException>(p.Check, "Should require either PublicIds or Prefix or Tag specified");
        }

        [Test]
        public void TestExplicitParamsCheck()
        {
            Assert.Throws<ArgumentException>(new ExplicitParams("").Check, "Should require PublicId");
        }

        [Test]
        public void TestExplodeParamsCheck()
        {
            var p = new ExplodeParams("", null);
            Assert.Throws<ArgumentException>(p.Check, "Should require PublicId");

            p.PublicId = "publicId";
            Assert.Throws<ArgumentException>(p.Check, "Should require Transformation");
        }

        [Test]
        public void TestGetResourceParamsCheck()
        {
            Assert.Throws<ArgumentException>(new GetResourceParams("").Check, "Should require PublicId");
        }

        [Test]
        public void TestMultiParamsCheck()
        {
            Assert.Throws<ArgumentException>(new MultiParams("").Check, "Should require Tag");
        }

        [Test]
        public void TestRawUploadParamsCheck()
        {
            var p = new RawUploadParams();
            Assert.Throws<ArgumentException>(p.Check, "Should require File");

            p.File = new FileDescription("", new MemoryStream());
            Assert.Throws<ArgumentException>(p.Check, "Should require FilePath and Stream specified for local file");
        }

        [Test]
        public void TestRenameParamsCheck()
        {
            var p = new RenameParams("", "");
            Assert.Throws<ArgumentException>(p.Check, "Should require FromPublicId");

            p.FromPublicId = "FromPublicId";
            Assert.Throws<ArgumentException>(p.Check, "Should require ToPublicId");
        }

        [Test]
        public void TestRestoreParamsCheck()
        {
            Assert.Throws<ArgumentException>(new RestoreParams().Check, "Should require at least one PublicId");
        }

        [Test]
        public void TestSpriteParamsCheck()
        {
            Assert.Throws<ArgumentException>(new SpriteParams("").Check, "Should require Tag");
        }

        [Test]
        public void TestTextParamsCheck()
        {
            Assert.Throws<ArgumentException>(new TextParams().Check, "Should require Text");
        }

        [Test]
        public void TestUpdateParamsCheck()
        {
            Assert.Throws<ArgumentException>(new UpdateParams("").Check, "Should require PublicId");
        }

        [Test]
        public void TestUpdateTransformParamsCheck()
        {
            Assert.Throws<ArgumentException>(new UpdateTransformParams().Check, "Should require Transformation");
        }

        [Test]
        public void UploadMappingParamsCheckTest()
        {
            var p = new UploadMappingParams { MaxResults = 1000 };
            Assert.Throws<ArgumentException>(p.Check, "Should require MaxResults value less or equal 500");
        }

        [Test]
        public void UploadPresetParamsCheckTest()
        {
            var p = new UploadPresetParams { Overwrite = true, Unsigned = true };
            Assert.Throws<ArgumentException>(p.Check, "Should require only one property set to true: Overwrite or Unsigned");
        }

        [Test]
        public void StreamingProfileCreateParamsCheckTest()
        {
            var p = new StreamingProfileCreateParams{Name = null};
            Assert.Throws<ArgumentException>(p.Check, "Should require Name");
        }

        [Test]
        public void StreamingProfileBaseParamsCheckTest()
        {
            var p = new StreamingProfileBaseParams { Representations = null };
            Assert.Throws<ArgumentException>(p.Check, "Should require Representations to not be null");

            p.Representations = new List<Representation>();
            Assert.Throws<ArgumentException>(p.Check, "Should require Representations to not be empty");
        }

        [Test]
        public void TestImageUploadParamsDictionary()
        {
            var parameters = new ImageUploadParams
            {
                CinemagraphAnalysis = true,
                Tags = TestConstants.TestTag
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual(TestConstants.TestTag, dictionary["tags"]);
            Assert.AreEqual("true", dictionary["cinemagraph_analysis"]);
        }

        [Test]
        public void TestUploadDynamicFoldersParamsCheck()
        {
            var parameters = new ImageUploadParams
            {
                Tags = TestConstants.TestTag,
                PublicIdPrefix = "fd_public_id_prefix",
                DisplayName = "test",
                UseFilenameAsDisplayName = true,
                AssetFolder = "asset_folder",
                UseAssetFolderAsPublicIdPrefix = true,
                Folder = "folder"
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual(TestConstants.TestTag, dictionary["tags"]);
            Assert.AreEqual("fd_public_id_prefix", dictionary["public_id_prefix"]);
            Assert.AreEqual("test", dictionary["display_name"]);
            Assert.AreEqual("true", dictionary["use_filename_as_display_name"]);
            Assert.AreEqual("asset_folder", dictionary["asset_folder"]);
            Assert.AreEqual("true", dictionary["use_asset_folder_as_public_id_prefix"]);
            Assert.AreEqual("folder", dictionary["folder"]);
        }

        [Test]
        public void TestExplicitDynamicFoldersParamsCheck()
        {
            var parameters = new ExplicitParams("test_id")
            {
                PublicIdPrefix = "fd_public_id_prefix",
                DisplayName = "test",
                UseFilenameAsDisplayName = true,
                AssetFolder = "asset_folder",
                UseAssetFolderAsPublicIdPrefix = true,
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual("fd_public_id_prefix", dictionary["public_id_prefix"]);
            Assert.AreEqual("test", dictionary["display_name"]);
            Assert.AreEqual("true", dictionary["use_filename_as_display_name"]);
            Assert.AreEqual("asset_folder", dictionary["asset_folder"]);
            Assert.AreEqual("true", dictionary["use_asset_folder_as_public_id_prefix"]);
        }

        [Test]
        public void TestExplicitParamsDictionary()
        {
            var parameters = new ExplicitParams(TestConstants.TestPublicId)
            {
                CinemagraphAnalysis = true
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual(TestConstants.TestPublicId, dictionary["public_id"]);
            Assert.AreEqual("true", dictionary["cinemagraph_analysis"]);
        }

        [Test]
        public void TestGetResourceParamsDictionary()
        {
            var parameters = new GetResourceParams(TestConstants.TestPublicId)
            {
                CinemagraphAnalysis = true
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual("true", dictionary["cinemagraph_analysis"]);
        }

        [Test]
        public void TestUpdateResourceParamsDictionary()
        {
            var parameters = new UpdateParams(TestConstants.TestPublicId)
            {
                Metadata = new StringDictionary
                {
                    {"metadataFieldId", "metadataValue"}
                },
                ClearInvalid = true
            };

            var dictionary = parameters.ToParamsDictionary();

            Assert.AreEqual("metadataFieldId=metadataValue", dictionary["metadata"]);
            Assert.AreEqual("true", dictionary["clear_invalid"]);
        }

        [Test]
        public void TestTypeInTagParams()
        {
            var parameters = new TagParams();
            parameters.Type = "some_type";
            Assert.AreEqual("some_type", parameters.ToParamsDictionary()["type"]);
        }
    }
}
